using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Utilities;
using HotelBooking.DataAccessLayer.Database;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class AuthService : IAuthService
{
    private readonly HotelContext _context;
    
    public AuthService(HotelContext context)
    {
        _context = context; 
    }
    public string GenerateJwt(LoginDto loginDto)
    {
        var guest = _context.Guests.FirstOrDefault(g => g.Email == loginDto.Email);

        if (guest == null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var passwordHash = PasswordHasher.HashPassword(loginDto.Password);
        var passwordIsOk = _context.Guests.FirstOrDefault(g => g.PasswordHash == passwordHash);

        if(passwordIsOk == null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, loginDto.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JWTKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _authSettings.JWTIssuer,
            audience: _authSettings.JWTIssuer, 
            claims: claims,
            expires: DateTime.Now.AddDays(_authSettings.JWTExpireDays), 
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
