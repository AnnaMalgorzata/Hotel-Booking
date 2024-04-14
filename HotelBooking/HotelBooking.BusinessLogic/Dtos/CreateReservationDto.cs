using System.ComponentModel.DataAnnotations;

namespace HotelBooking.BusinessLogic.Dtos;

public class CreateReservationDto
{
    public string GuestEmail { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public ICollection<BasicRoomInfo> Rooms { get; set; }
}
