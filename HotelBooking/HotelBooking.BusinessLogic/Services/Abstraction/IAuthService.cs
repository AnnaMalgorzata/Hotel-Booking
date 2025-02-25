using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IAuthService
{
    string GenerateJwt(LoginDto loginDto);
}
