using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EtkinlikApi0.Data;
using EtkinlikApi0.Models;

namespace EtkinlikApi0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ REGISTER (Kayıt Ol)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            // Aynı email zaten var mı?
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Bu e-posta adresi zaten kayıtlı." });

            // Şifreyi basit tutuyoruz, istersen hashing ekleriz
            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kayıt başarılı!", user = newUser });
        }

        // ✅ LOGIN (Giriş Yap)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == request.Email.ToLower() &&
                    u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { message = "E-posta veya şifre hatalı." });

            return Ok(new { message = "Giriş başarılı!", user });
        }
            }
}
