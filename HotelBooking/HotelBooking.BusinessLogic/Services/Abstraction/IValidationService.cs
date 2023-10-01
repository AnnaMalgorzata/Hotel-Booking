namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IValidationService<TRequest> where TRequest : class
{
    public Task Validate(TRequest request);
}
