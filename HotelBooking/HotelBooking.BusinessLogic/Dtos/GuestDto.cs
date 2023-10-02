namespace HotelBooking.BusinessLogic.Dtos;
public class GuestDto
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateBirth { get; set; }

/*    public GuestDto(string Firstname, string Lastname, string Email, string PhoneNumber, DateTime DateBirth) 
    {
        this.Firstname = Firstname;
        this.Lastname = Lastname;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.DateBirth = DateBirth;
    }*/

}
