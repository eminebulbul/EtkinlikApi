using Microsoft.AspNetCore.Mvc;

namespace EtkinlikApi0.Controllers // Proje ismine göre namespace'i ayarla
{
    // --- Önceki DTO tanımlamalarını buraya veya ayrı bir Models klasörüne ekle ---
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
    }
    // --- DTO Tanımlamaları Bitiş ---


    [Route("api/[controller]")] // -> /api/Auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        // TODO: Servisleri inject et (IAuthService gibi)

        [HttpPost("Register")] // POST /api/Auth/Register
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // TODO: Kayıt işlemleri
            Console.WriteLine($"Register attempt: {request.Email}"); // Konsola log yazalım
            // Şimdilik başarılı varsay
            return Ok(new { message = "Kayıt başarılı (geçici)" });
        }

        [HttpPost("Login")] // POST /api/Auth/Login
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // TODO: Giriş işlemleri
             Console.WriteLine($"Login attempt: {request.Email}"); // Konsola log yazalım
            // Şimdilik başarılı varsayıp sahte token dön
            var fakeToken = "sahte_jwt_token_" + Guid.NewGuid().ToString();
            return Ok(new LoginResponseDto { Token = fakeToken });
        }
    }
}