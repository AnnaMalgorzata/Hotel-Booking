using FluentValidation;
using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Validators;
internal class ReservationValidator : AbstractValidator<CreateReservationDto>
{
    public ReservationValidator()
    {
        RuleFor(createReservationDto => createReservationDto.GuestEmail).NotEmpty().EmailAddress().WithMessage("Email address is required.");
        
        RuleFor(dto => dto.DateFrom)
             .NotEmpty().WithMessage("Start date is required.")
             .GreaterThan(DateTime.Now).WithMessage("The start date must be the future.");

        RuleFor(dto => dto.DateTo)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(dto => dto.DateFrom).WithMessage("The end date must be later than the begin date.")
            .LessThanOrEqualTo(dto => dto.DateFrom.AddDays(30)).WithMessage("The duration of the stay cannot be longer than 30 nights.");

        RuleFor(dto => dto.Rooms).NotEmpty().WithMessage("Room choice is required.");

    }
}