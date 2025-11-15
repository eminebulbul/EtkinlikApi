namespace EtkinlikApi0.Models
{
    public class ConversationDto
    {
        public int Id { get; set; }

        public int UserAId { get; set; }
        public string UserAName { get; set; } = string.Empty;

        public int UserBId { get; set; }
        public string UserBName { get; set; } = string.Empty;

        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
