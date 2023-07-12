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

        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=HotelBooking;User Id=sa;Password=Secret@Passw0rd;");

    }

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(
            g =>
            {
                g.Property(g => g.PhoneNumber).HasMaxLength(9);
                g.Property(g => g.Email).HasMaxLength(100);
                g.HasIndex(g=> g.Email).IsUnique();
                g.ToTable("Guests");
            });

        modelBuilder.Entity<Reservation>(
            re =>
            {
                re.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");
                re.ToTable("Reservations");
            });

        modelBuilder.Entity<Room>(
            ro =>
            {
                ro.HasIndex(ro => ro.Number).IsUnique();
                ro.Property(e => e.Type)
                    .HasConversion(
                        v => v.ToString(),
                        v => (RoomType)Enum.Parse(typeof(RoomType), v))
                    .HasColumnType("nvarchar(20)");
                ro.Property(e => e.PricePerNight)
                    .HasColumnType("decimal(18,2)");
                ro.ToTable("Rooms");
            });

        modelBuilder.Entity<Reservation>()
            .HasOne<Guest>(s => s.Guest)
            .WithMany(g => g.Reservations)
            .HasForeignKey(s => s.GuestId);

        modelBuilder.Entity<Room>()
           .HasMany<Reservation>(s => s.Reservations)
           .WithMany(g => g.Rooms)
           .UsingEntity<Dictionary<string, object>>("RoomsReservations",
                x => x.HasOne<Reservation>().WithMany().HasForeignKey("ReservationId"),
                x => x.HasOne<Room>().WithMany().HasForeignKey("RoomId"),
                x => x.ToTable("RoomsReservations"));
    }
}

