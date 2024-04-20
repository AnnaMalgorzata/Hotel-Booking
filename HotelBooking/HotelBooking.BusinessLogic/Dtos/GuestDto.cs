using System.ComponentModel.DataAnnotations;

namespace HotelBooking.BusinessLogic.Dtos;
public class GuestDto
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateOnly DateBirth { get; set; }

}
