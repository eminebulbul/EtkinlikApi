namespace EtkinlikApi0.Models // Proje ismine göre namespace'i ayarla
{
    public class EventDto
    {
        public int Id { get; set; }
        public required string Title { get; set; } // required C# 11+ özelliği, eski sürümse kaldırabilirsin
        public required string ImageUrl { get; set; }
        public required string PeopleNeeded { get; set; }
        public required string HostName { get; set; }
        public required string HostImageUrl { get; set; }
        public DateTime Date { get; set; } // Flutter tarafı String beklese de C#'ta DateTime kullanmak daha doğru. Dönüşüm API'de yapılır.
        public required string Location { get; set; }
    }
}