namespace HotelBooking.DataAccessLayer.Entities;
public class Room : Entity
{
    public int RoomId { get; set; }
    public int Number { get; set; }
    public int Floor { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}
