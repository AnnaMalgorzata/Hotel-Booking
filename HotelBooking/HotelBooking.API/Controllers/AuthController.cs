using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelBooking.API;
using Microsoft.Extensions.Options;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;

namespace API.Controllers // Upewnij się, że przestrzeń nazw jest zgodna z Twoją strukturą.
{
    [Route("authentication")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            string token = _authService.GenerateJwt(loginDto);
            


            return Unauthorized();
        }
    }
}
