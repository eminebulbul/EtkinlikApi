using Microsoft.EntityFrameworkCore;
using EtkinlikApi0.Models;

namespace EtkinlikApi0.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.Property(u => u.Id).HasColumnName("Id");
            entity.Property(u => u.Name).HasColumnName("Name");
            entity.Property(u => u.Email).HasColumnName("Email");
            entity.Property(u => u.Password).HasColumnName("Password");
            entity.Property(u => u.CreatedAt).HasColumnName("CreatedAt");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("events");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Title).HasColumnName("Title");
            entity.Property(e => e.Description).HasColumnName("Description");
            entity.Property(e => e.Date).HasColumnName("Date");
            entity.Property(e => e.Location).HasColumnName("Location");
            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerId");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.ToTable("participants");
            entity.Property(p => p.Id).HasColumnName("Id");
            entity.Property(p => p.EventId).HasColumnName("EventId");
            entity.Property(p => p.UserId).HasColumnName("UserId");
            entity.Property(p => p.JoinedAt).HasColumnName("JoinedAt");
        });
    }



    }
}
