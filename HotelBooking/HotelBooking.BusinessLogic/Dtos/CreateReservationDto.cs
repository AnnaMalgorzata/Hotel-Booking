namespace HotelBooking.BusinessLogic.Dtos;

public class CreateReservationDto
{
    public string GuestEmail { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public ICollection<BasicRoomInfo> Rooms { get; set; }
}
