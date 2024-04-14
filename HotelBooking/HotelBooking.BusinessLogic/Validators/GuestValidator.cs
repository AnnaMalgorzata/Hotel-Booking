using FluentValidation;
using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Validators;
public class GuestValidator : AbstractValidator<GuestDto>
{
    public GuestValidator()
    {
        RuleFor(guestDto => guestDto.Firstname).NotEmpty().WithMessage("Firstname is required");
        RuleFor(guestDto => guestDto.Lastname).NotEmpty().WithMessage("Lastname is required");
        RuleFor(guestDto => guestDto.Email).EmailAddress().WithMessage("Email address is required.");
        RuleFor(guestDto => guestDto.PhoneNumber).Matches("^([0-9]{7,14})$").WithMessage("Required phone number format.");
        RuleFor(guestDto => guestDto.DateBirth.AddYears(18)).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Age of majority required.");
    }
}
