using FluentValidation;
using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Validators;
public class RegistrationValidator : AbstractValidator<RegistrationDto>
{
    public RegistrationValidator()
    {
        RuleFor(guestDto => guestDto.Firstname).NotEmpty().WithMessage("Firstname is required");
        RuleFor(guestDto => guestDto.Lastname).NotEmpty().WithMessage("Lastname is required");
        RuleFor(guestDto => guestDto.Email).EmailAddress().WithMessage("Email address is required.");
        RuleFor(guestDto => guestDto.PhoneNumber).Matches("^([0-9]{7,14})$").WithMessage("Required phone number format.");
        RuleFor(guestDto => guestDto.DateBirth.AddYears(18)).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Age of majority required.");
        RuleFor(guestDto => guestDto.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
    }
}
