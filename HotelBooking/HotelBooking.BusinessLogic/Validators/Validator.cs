using FluentValidation;

namespace HotelBooking.BusinessLogic.Validators;
public class Validator<T> : AbstractValidator<T> where T : class
{
    public Validator()
    {
        
    }
}

