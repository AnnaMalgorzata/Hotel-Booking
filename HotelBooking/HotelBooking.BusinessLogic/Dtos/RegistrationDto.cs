using System.Diagnostics.CodeAnalysis;

namespace HotelBooking.BusinessLogic.Dtos;
public class RegistrationDto
{
    public string Firstname { get; init; }

    public string Lastname { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public DateOnly DateBirth { get; init; }

    public string Password { get; init; }

    public string PasswordConfirmation { get; init; }

}
