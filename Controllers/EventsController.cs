using Microsoft.AspNetCore.Mvc;
using EtkinlikApi0.Models; // EventDto için using ekle
using System; // DateTime için
using System.Collections.Generic; // List için
using System.Linq; // Select (LINQ) için

namespace EtkinlikApi0.Controllers // Proje ismine göre namespace'i ayarla
{
    [Route("api/[controller]")] // -> /api/Events
    [ApiController]
    public class EventsController : ControllerBase
    {
        // Şimdilik sahte, statik bir etkinlik listesi oluşturalım
        private static List<EventDto> _sampleEvents = new List<EventDto>
        {
            new EventDto {
                Id = 1,
                Title = "Okey Oynayacak 4. Aranıyor!",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Okey+API",
                PeopleNeeded = "1",
                HostName = "Ahmet Yılmaz (API)",
                HostImageUrl = "https://i.pravatar.cc/150?img=1",
                Date = DateTime.Now.AddDays(2).AddHours(5), // Örnek tarih
                Location = "Starbucks API"
            },
            new EventDto {
                Id = 2,
                Title = "Halı Saha Maçı İçin Oyuncu (API)",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Futbol+API",
                PeopleNeeded = "2",
                HostName = "Mehmet Demir",
                HostImageUrl = "https://i.pravatar.cc/150?img=5",
                Date = DateTime.Now.AddDays(3).AddHours(7),
                Location = "Olimpik Halı Saha"
            },
             new EventDto {
                Id = 3,
                Title = "Sinemada Film İzleme Etkinliği",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Sinema+API",
                PeopleNeeded = "3+",
                HostName = "Ayşe Kaya",
                HostImageUrl = "https://i.pravatar.cc/150?img=8",
                Date = DateTime.Now.AddDays(4).AddHours(6).AddMinutes(30),
                Location = "Ankamall Cinemaximum"
             }
        };

        // GET /api/Events
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetEvents()
        {
            Console.WriteLine("GET /api/Events isteği geldi."); // Konsola log yazalım

            // Flutter'ın beklediği formata dönüştürerek gönderelim
            // Özellikle DateTime -> String formatlaması önemli
            var formattedEvents = _sampleEvents.Select(e => new {
                e.Id,
                e.Title,
                e.ImageUrl,
                e.PeopleNeeded,
                e.HostName,
                e.HostImageUrl,
                Date = e.Date, // Flutter tarafı DateTime.parse ile hallediyor
                e.Location
            });

            return Ok(formattedEvents);
        }

        // TODO: Diğer CRUD operasyonları eklenebilir (GetById, Post, Put, Delete)
        // TODO: Bu endpoint'i [Authorize] attribute'u ile koruma altına al (Giriş yapıldıktan sonra)
    }
}