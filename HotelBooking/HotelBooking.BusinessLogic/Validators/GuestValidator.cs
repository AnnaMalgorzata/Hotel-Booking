using FluentValidation;
using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Validators;
public class GuestValidator : AbstractValidator<GuestDto>
{
    public GuestValidator()
    {
        RuleFor(guestDto => guestDto.Firstname).NotEmpty().WithMessage("firstname");
        RuleFor(guestDto => guestDto.Lastname).NotEmpty().WithMessage("lastname");
        RuleFor(guestDto => guestDto.Email).EmailAddress().WithMessage("email");
        RuleFor(guestDto => guestDto.PhoneNumber).Matches("^([0-9]{7,14})$").WithMessage("phone number");
        RuleFor(guestDto => guestDto.DateBirth.AddYears(18)).LessThanOrEqualTo(DateTime.Today).WithMessage("date birth - you're underage");
    }
}
