namespace EtkinlikApi0.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }

        public int FromUserId { get; set; }
        public string FromUserName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
