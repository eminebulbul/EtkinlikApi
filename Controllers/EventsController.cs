using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EtkinlikApi0.Models;

namespace EtkinlikApi0.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/Events
    public class EventsController : ControllerBase
    {
        // Şimdilik in-memory liste (sunucu restart olunca sıfırlanır)
        private static readonly List<EventDto> _events = new()
        {
            new EventDto
            {
                Id = 1,
                Title = "Okey Oynayacak 4. Aranıyor!",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Okey",
                PeopleNeeded = "1",
                HostName = "Ahmet Yılmaz",
                HostImageUrl = "https://i.pravatar.cc/150?img=1",
                Date = DateTime.UtcNow.AddDays(1).AddHours(3),
                Location = "Bahçelievler Starbucks",
                OrganizerUserId = 1
            },
            new EventDto
            {
                Id = 2,
                Title = "Sinema - Dune 2 Gecesi",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Sinema",
                PeopleNeeded = "2",
                HostName = "Zeynep Kaya",
                HostImageUrl = "https://i.pravatar.cc/150?img=2",
                Date = DateTime.UtcNow.AddDays(3).AddHours(2),
                Location = "Armada Cinemaximum",
                OrganizerUserId = 2
            },
            new EventDto
            {
                Id = 3,
                Title = "Sabah Koşusu - Botanik Park",
                ImageUrl = "https://via.placeholder.com/300x150.png?text=Ko%C5%9Fu",
                PeopleNeeded = "3",
                HostName = "Emre Demir",
                HostImageUrl = "https://i.pravatar.cc/150?img=3",
                Date = DateTime.UtcNow.AddDays(2).AddHours(7),
                Location = "Ankara Botanik Parkı",
                OrganizerUserId = 3
            }
        };

        // GET: /api/Events
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAllEvents()
        {
            var formatted = _events.Select(e => new
            {
                id = e.Id,
                title = e.Title,
                imageUrl = e.ImageUrl,
                peopleNeeded = e.PeopleNeeded,
                hostName = e.HostName,
                hostImageUrl = e.HostImageUrl,
                date = e.Date,
                location = e.Location,
                organizerUserId = e.OrganizerUserId
            });

            return Ok(formatted);
        }

        // POST: /api/Events
        // Flutter AddEventScreen buraya POST atıyor
        [HttpPost]
        public ActionResult<object> CreateEvent([FromBody] EventDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Geçersiz istek gövdesi." });
            }

            var newId = _events.Any() ? _events.Max(e => e.Id) + 1 : 1;

            var dto = new EventDto
            {
                Id = newId,
                Title = request.Title ?? string.Empty,
                ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl)
                    ? "https://via.placeholder.com/300x150.png?text=Etkinlik"
                    : request.ImageUrl,
                PeopleNeeded = string.IsNullOrWhiteSpace(request.PeopleNeeded)
                    ? "1"
                    : request.PeopleNeeded,
                HostName = string.IsNullOrWhiteSpace(request.HostName)
                    ? "Bilinmeyen Ev Sahibi"
                    : request.HostName,
                HostImageUrl = string.IsNullOrWhiteSpace(request.HostImageUrl)
                    ? "https://i.pravatar.cc/150?img=11"
                    : request.HostImageUrl,
                Date = request.Date == default
                    ? DateTime.UtcNow.AddDays(1)
                    : request.Date,
                Location = string.IsNullOrWhiteSpace(request.Location)
                    ? "Belirtilmemiş konum"
                    : request.Location,
                OrganizerUserId = request.OrganizerUserId
            };

            _events.Add(dto);

            var response = new
            {
                id = dto.Id,
                title = dto.Title,
                imageUrl = dto.ImageUrl,
                peopleNeeded = dto.PeopleNeeded,
                hostName = dto.HostName,
                hostImageUrl = dto.HostImageUrl,
                date = dto.Date,
                location = dto.Location,
                organizerUserId = dto.OrganizerUserId
            };

            return CreatedAtAction(nameof(GetAllEvents), new { id = dto.Id }, response);
        }
    }
}
