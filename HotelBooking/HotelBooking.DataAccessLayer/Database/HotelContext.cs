using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAccessLayer.Database;

public class HotelContext : DbContext
{
    public HotelContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=HotelBooking;Trusted_Connection=True;");

    }

    public DbSet<Dictionary<string, object>> Guests => Set<Dictionary<string, object>>("Guest");
    public DbSet<Dictionary<string, object>> Reservations => Set<Dictionary<string, object>>("Reservation");
    public DbSet<Dictionary<string, object>> Rooms => Set<Dictionary<string, object>>("Room");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(
            g =>
            {
                g.Property(g => g.GuestId)
                    .HasColumnName("guest_id")
                    .HasColumnType("int");
                g.Property(g => g.Firstname)
                    .HasColumnName("firstname")
                    .HasColumnType("string");
                g.Property(g => g.Lastname)
                    .HasColumnName("lastname")
                    .HasColumnType("string");
                g.Property(g => g.Email)
                    .HasColumnName("email")
                    .HasColumnType("string");
                g.Property(g => g.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasColumnType("string");
                g.Property(g => g.DateBirth)
                    .HasColumnName("date_birth")
                    .HasColumnType("datetime");
            });

        modelBuilder.Entity<Reservation>(
            re =>
            {
                re.Property(e => e.ReservationId)
                    .HasColumnName("reservation_id")
                    .HasColumnType("int");
                re.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("datetime");
                re.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("datetime");
                re.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18,2)");
                re.Property(e => e.CurrentGuestId)
                    .HasColumnName("guest_id")
                    .HasColumnType("int");
            });

        modelBuilder.Entity<Room>(
            ro =>
            {
                ro.Property(e => e.RoomId)
                    .HasColumnName("room_id")
                    .HasColumnType("int");
                ro.Property(e => e.Number)
                    .HasColumnName("room_number")
                    .HasColumnType("int");
                ro.Property(e => e.Floor)
                    .HasColumnName("floor")
                    .HasColumnType("int");
                ro.Property(e => e.Type)
                    .HasConversion(
                        v => v.ToString(),
                        v => (RoomType)Enum.Parse(typeof(RoomType), v))
                    .HasColumnName("room_type")
                    .HasColumnType("nvarchar(Max)");
                ro.Property(e => e.Capacity)
                    .HasColumnName("capacity")
                    .HasColumnType("int");
                ro.Property(e => e.PricePerNight)
                    .HasColumnName("price_per_night")
                    .HasColumnType("decimal(18,2)");
            });

        modelBuilder.Entity<Reservation>()
            .HasOne<Guest>(s => s.Guest)
            .WithMany(g => g.Reservations)
            .HasForeignKey(s => s.CurrentGuestId);

        modelBuilder.Entity<Room>()
           .ToTable("rooms")
           .HasMany<Reservation>(s => s.Reservations)
           .WithMany(g => g.Rooms);

        modelBuilder.Entity<Guest>()
           .ToTable("guests")
           .HasMany<Room>(s => s.Rooms)
           .WithMany(g => g.Guests);
    }
}

