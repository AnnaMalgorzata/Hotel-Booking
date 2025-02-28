using HotelBooking.BusinessLogic.Services.Abstraction;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class JWTService : IJWTService
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JWTService(IOptions<AuthenticationSettings> authenticationSettings)
    {
        _authenticationSettings = authenticationSettings.Value;
    }

    public string GenerateJWT(string email)
    {
        var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
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
