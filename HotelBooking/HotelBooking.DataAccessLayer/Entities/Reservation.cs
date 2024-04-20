namespace HotelBooking.DataAccessLayer.Entities;
public class Reservation : Entity
{
    public int ReservationId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public decimal Price { get; set; }

    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public ICollection<Room> Rooms { get; set; }
}
