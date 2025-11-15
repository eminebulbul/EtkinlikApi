using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EtkinlikApi0.Models;

namespace EtkinlikApi0.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/Conversations
    public class ConversationsController : ControllerBase
    {
        // In-memory conversation & message store
        private static readonly List<ConversationDto> _conversations = new();
        private static readonly List<MessageDto> _messages = new();

        public class StartConversationBody
        {
            public int UserAId { get; set; }
            public string UserAName { get; set; } = string.Empty;
            public int UserBId { get; set; }
            public string UserBName { get; set; } = string.Empty;
            public int EventId { get; set; }
            public string EventTitle { get; set; } = string.Empty;
        }

        public class SendMessageBody
        {
            public int FromUserId { get; set; }
            public string FromUserName { get; set; } = string.Empty;
            public string Text { get; set; } = string.Empty;
        }

        // POST: /api/Conversations/start
        // Body: { userAId, userAName, userBId, userBName, eventId, eventTitle }
        // Aynı ikili + aynı event için zaten varsa onu döner, yoksa yeni conversation yaratır.
        [HttpPost("start")]
        public ActionResult<object> StartConversation([FromBody] StartConversationBody body)
        {
            if (body == null)
            {
                return BadRequest(new { message = "Geçersiz istek gövdesi." });
            }

            // Var mı aynı ikili + aynı event? (sıra önemli değil)
            var existing = _conversations.FirstOrDefault(c =>
                c.EventId == body.EventId &&
                ((c.UserAId == body.UserAId && c.UserBId == body.UserBId) ||
                 (c.UserAId == body.UserBId && c.UserBId == body.UserAId)));

            if (existing != null)
            {
                return Ok(new
                {
                    id = existing.Id,
                    userAId = existing.UserAId,
                    userAName = existing.UserAName,
                    userBId = existing.UserBId,
                    userBName = existing.UserBName,
                    eventId = existing.EventId,
                    eventTitle = existing.EventTitle
                });
            }

            var newId = _conversations.Any() ? _conversations.Max(c => c.Id) + 1 : 1;

            var conv = new ConversationDto
            {
                Id = newId,
                UserAId = body.UserAId,
                UserAName = body.UserAName ?? string.Empty,
                UserBId = body.UserBId,
                UserBName = body.UserBName ?? string.Empty,
                EventId = body.EventId,
                EventTitle = body.EventTitle ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _conversations.Add(conv);

            return Ok(new
            {
                id = conv.Id,
                userAId = conv.UserAId,
                userAName = conv.UserAName,
                userBId = conv.UserBId,
                userBName = conv.UserBName,
                eventId = conv.EventId,
                eventTitle = conv.EventTitle
            });
        }

        // GET: /api/Conversations/{conversationId}/messages
        [HttpGet("{conversationId:int}/messages")]
        public ActionResult<IEnumerable<object>> GetMessages(int conversationId)
        {
            var list = _messages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.SentAt)
                .Select(m => new
                {
                    id = m.Id,
                    conversationId = m.ConversationId,
                    fromUserId = m.FromUserId,
                    fromUserName = m.FromUserName,
                    text = m.Text,
                    sentAt = m.SentAt
                });

            return Ok(list);
        }

        // POST: /api/Conversations/{conversationId}/messages
        // Body: { fromUserId, fromUserName, text }
        [HttpPost("{conversationId:int}/messages")]
        public ActionResult<object> SendMessage(int conversationId, [FromBody] SendMessageBody body)
        {
            if (body == null || string.IsNullOrWhiteSpace(body.Text))
            {
                return BadRequest(new { message = "Mesaj metni boş olamaz." });
            }

            var conv = _conversations.FirstOrDefault(c => c.Id == conversationId);
            if (conv == null)
            {
                return NotFound(new { message = "Konuşma bulunamadı." });
            }

            var newId = _messages.Any() ? _messages.Max(m => m.Id) + 1 : 1;

            var msg = new MessageDto
            {
                Id = newId,
                ConversationId = conversationId,
                FromUserId = body.FromUserId,
                FromUserName = body.FromUserName ?? string.Empty,
                Text = body.Text ?? string.Empty,
                SentAt = DateTime.UtcNow
            };

            _messages.Add(msg);

            return Ok(new
            {
                id = msg.Id,
                conversationId = msg.ConversationId,
                fromUserId = msg.FromUserId,
                fromUserName = msg.FromUserName,
                text = msg.Text,
                sentAt = msg.SentAt
            });
        }
    }
}
