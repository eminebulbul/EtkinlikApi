namespace EtkinlikApi0.Models
{
    public class JoinRequestDto
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;

        public int FromUserId { get; set; }
        public string FromUserName { get; set; } = string.Empty;

        public int ToUserId { get; set; } // Etkinlik sahibi

        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RespondedAt { get; set; }
    }
}
