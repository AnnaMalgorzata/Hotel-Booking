using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IAuthService
{
    Task<string> Login(LoginDto loginDto);
}
