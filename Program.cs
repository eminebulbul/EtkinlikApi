using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EtkinlikApi0.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Kestrel'i dışarı aç: mobil emulator / Chrome bağlanabilsin
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5221); // http://localhost:5221 ve dışarıdan 5221
});

// CORS politikası: Flutter web'in farklı origin'den çağırmasına izin ver
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller'ları ekle
builder.Services.AddControllers();

var app = builder.Build();

// CORS middleware en başlarda olsun
app.UseCors("AllowAll");

// Development ise Swagger / OpenAPI gibi şeyleri burada map edebilirsin
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); // varsa
}

// HTTPS redirect şimdilik kapalı. Flutter HTTP ile çağırıyor.
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
