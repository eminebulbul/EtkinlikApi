namespace EtkinlikApi0.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string PeopleNeeded { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string HostImageUrl { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;

        // Etkinliği oluşturan kullanıcının Id'si
        public int OrganizerUserId { get; set; }
    }
}
