using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EtkinlikApi0.Models;

namespace EtkinlikApi0.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/JoinRequests
    public class JoinRequestsController : ControllerBase
    {
        // In-memory join request list
        private static readonly List<JoinRequestDto> _requests = new();

        public class RespondBody
        {
            public bool Accept { get; set; }
        }

        // POST: /api/JoinRequests
        // Body: { eventId, eventTitle, fromUserId, fromUserName, toUserId }
        [HttpPost]
        public ActionResult<object> Create([FromBody] JoinRequestDto body)
        {
            if (body == null)
            {
                return BadRequest(new { message = "Geçersiz istek gövdesi." });
            }

            // 🔒 Kendi etkinliğine katılmayı backend'de de engelle
            if (body.FromUserId == body.ToUserId)
            {
                return BadRequest(new { message = "Kendi oluşturduğun etkinliğe katılamazsın." });
            }

            var newId = _requests.Any() ? _requests.Max(r => r.Id) + 1 : 1;

            var dto = new JoinRequestDto
            {
                Id = newId,
                EventId = body.EventId,
                EventTitle = body.EventTitle ?? string.Empty,
                FromUserId = body.FromUserId,
                FromUserName = body.FromUserName ?? string.Empty,
                ToUserId = body.ToUserId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _requests.Add(dto);

            var response = new
            {
                id = dto.Id,
                eventId = dto.EventId,
                eventTitle = dto.EventTitle,
                fromUserId = dto.FromUserId,
                fromUserName = dto.FromUserName,
                toUserId = dto.ToUserId,
                status = dto.Status,
                createdAt = dto.CreatedAt
            };

            return CreatedAtAction(nameof(GetIncomingForUser), new { userId = dto.ToUserId }, response);
        }

        // GET: /api/JoinRequests/incoming/{userId}
        // İLAN SAHİBİNE GELEN BEKLEYEN İSTEKLER
        [HttpGet("incoming/{userId:int}")]
        public ActionResult<IEnumerable<object>> GetIncomingForUser(int userId)
        {
            var pending = _requests
                .Where(r => r.ToUserId == userId && r.Status == "Pending")
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new
                {
                    id = r.Id,
                    eventId = r.EventId,
                    eventTitle = r.EventTitle,
                    fromUserId = r.FromUserId,
                    fromUserName = r.FromUserName,
                    toUserId = r.ToUserId,
                    status = r.Status,
                    createdAt = r.CreatedAt
                });

            return Ok(pending);
        }

        // GET: /api/JoinRequests/outgoing/{userId}
        // BENİM ATTIĞIM TÜM İSTEKLER (Bekliyor + Kabul + Reddedildi)
        [HttpGet("outgoing/{userId:int}")]
        public ActionResult<IEnumerable<object>> GetOutgoingForUser(int userId)
        {
            var list = _requests
                .Where(r => r.FromUserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new
                {
                    id = r.Id,
                    eventId = r.EventId,
                    eventTitle = r.EventTitle,
                    fromUserId = r.FromUserId,
                    fromUserName = r.FromUserName,
                    toUserId = r.ToUserId,
                    status = r.Status,
                    createdAt = r.CreatedAt
                });

            return Ok(list);
        }

        // POST: /api/JoinRequests/{id}/respond
        // Body: { "accept": true/false }
        [HttpPost("{id:int}/respond")]
        public ActionResult<object> Respond(int id, [FromBody] RespondBody body)
        {
            var request = _requests.FirstOrDefault(r => r.Id == id);
            if (request == null)
            {
                return NotFound(new { message = "İstek bulunamadı." });
            }

            if (request.Status != "Pending")
            {
                return BadRequest(new { message = "Bu istek zaten yanıtlanmış." });
            }

            request.Status = body.Accept ? "Accepted" : "Rejected";
            request.RespondedAt = DateTime.UtcNow;

            var response = new
            {
                id = request.Id,
                eventId = request.EventId,
                fromUserId = request.FromUserId,
                toUserId = request.ToUserId,
                status = request.Status,
                respondedAt = request.RespondedAt
            };

            return Ok(response);
        }
    }
}
