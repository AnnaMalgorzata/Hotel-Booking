namespace HotelBooking.DataAccessLayer.Entities;
public class Guest : Entity
{
    public int GuestId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateBirth { get; set; }

    public ICollection<Reservation> Reservations { get; set; }    
}
