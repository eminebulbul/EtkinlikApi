namespace EtkinlikApi0.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }

        public int OrganizerId { get; set; }
        public User? Organizer { get; set; }

        // Navigation property
        public ICollection<Participant>? Participants { get; set; }
    }
}
