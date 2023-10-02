using FluentValidation;
using FluentValidation.Results;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using System.Text;

namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class ValidationService<TRequest> : IValidationService<TRequest> where TRequest : class
{

    private readonly IValidator<TRequest> _validator;

    public ValidationService(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task Validate(TRequest request)
    {
        
        ValidationResult result = await _validator.ValidateAsync(request);
        var message = new StringBuilder();

        if (!result.IsValid)
        {
            message.Append("Invalid fields: ");

            foreach (var failure in result.Errors)
            {
                message.Append(failure.ErrorMessage + ", ");
            }

            throw new BadRequestException(message.Remove(message.Length - 2, 2).ToString());

        }
    }
}
