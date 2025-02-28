using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Utilities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class AuthService : IAuthService
{
    private readonly IGuestRepository _guestRepository;
    private readonly IJWTService _jwtService;

    public AuthService(IGuestRepository guestRepository, IJWTService jwtService)
    {
        _guestRepository = guestRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var guest = await _guestRepository.GetGuest(loginDto.Email);

        if (guest == null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        bool verified = PasswordHasher.Verify(loginDto.Password, guest.PasswordHash);
        
        if (!verified)
        {
            throw new BadRequestException("Invalid username or password :(");
        }

        var token = _jwtService.GenerateJWT(loginDto.Email);

        return token;
    }
}
