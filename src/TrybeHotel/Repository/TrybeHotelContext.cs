using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;

namespace TrybeHotel.Repository;
public class TrybeHotelContext : DbContext, ITrybeHotelContext
{
    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options) {
        Seeder.SeedUserAdmin(this);
    }
    public TrybeHotelContext() { }

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if(!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost;Database=TrybeHotel;User=SA;Password=SqlServer123!;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<City>()
            .HasMany(c => c.Hotels)
            .WithOne(h => h.City)
            .HasForeignKey(h => h.CityId);

        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId);
    }

}