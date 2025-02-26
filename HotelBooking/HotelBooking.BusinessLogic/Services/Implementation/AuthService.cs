using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Utilities;
using HotelBooking.DataAccessLayer.Database;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class AuthService : IAuthService
{
    private readonly HotelContext _context; 
    private readonly AuthenticationSettings _authenticationSettings;

    public AuthService(HotelContext context, IOptions<AuthenticationSettings> authenticationSettings)
    {
        _context = context; 
        _authenticationSettings = authenticationSettings.Value;
    }

    public string Login(LoginDto loginDto) //nazwa login
    {
        var guest = _context.Guests.FirstOrDefault(g => g.Email == loginDto.Email);

        if (guest == null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var passwordHash = "soL/lHRA7/aUlEPaD+Vo+DGJVDXoWzgDzrvveofO9CI=";//PasswordHasher.HashPassword(loginDto.Password); -> to samo hasło hashuje inaczej 
        var passwordIsOk = _context.Guests.Any(g => g.PasswordHash == passwordHash);

        if(!passwordIsOk)
        {
            throw new BadRequestException("Invalid username or password");
        }

        //Generowanie tokena:
        var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, loginDto.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JWTKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _authenticationSettings.JWTIssuer,
            audience: _authenticationSettings.JWTIssuer, 
            claims: claims,
            expires: DateTime.Now.AddDays(_authenticationSettings.JWTExpireDays), 
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
