using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EtkinlikApi0.Controllers
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string PeopleNeeded { get; set; }
        public string HostName { get; set; }
        public string HostImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }

    [Route("api/[controller]")] // /api/Events
    [ApiController]
    public class EventsController : ControllerBase
    {
        // Şu anlık sahte veri
        private static readonly List<EventDto> _sampleEvents = new()
        {
            new EventDto {
                Id = 1,
                Title = "Okey Oynayacak 4. Aranıyor!",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Okey+API",
                PeopleNeeded = "1",
                HostName = "Ahmet Yılmaz (API)",
                HostImageUrl = "https://i.pravatar.cc/150?img=1",
                Date = DateTime.Now.AddDays(2).AddHours(5),
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

        [HttpGet] // GET /api/Events
        public ActionResult<IEnumerable<object>> GetEvents()
        {
            Console.WriteLine("GET /api/Events isteği geldi.");

            // Flutter'ın beklentisine uygun (DateTime -> ISO string parse edilebilir)
            var formatted = _sampleEvents.Select(e => new {
                id = e.Id,
                title = e.Title,
                imageUrl = e.ImageUrl,
                peopleNeeded = e.PeopleNeeded,
                hostName = e.HostName,
                hostImageUrl = e.HostImageUrl,
                date = e.Date, // Flutter tarafında DateTime.parse() yapıyoruz
                location = e.Location
            });

            return Ok(formatted);
        }
    }
}
