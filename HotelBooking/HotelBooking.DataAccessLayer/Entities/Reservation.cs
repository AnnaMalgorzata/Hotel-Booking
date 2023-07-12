using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Reservation
{
    public int ReservationId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public decimal Price { get; set; }

    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public ICollection<Room> Rooms { get; set; }
}
