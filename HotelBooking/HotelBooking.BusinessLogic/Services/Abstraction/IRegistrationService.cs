using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IRegistrationService
{
    public Task RegisterGuest(RegistrationDto guestDto);
}
