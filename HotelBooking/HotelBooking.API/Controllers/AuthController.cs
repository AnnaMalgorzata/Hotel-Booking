using Microsoft.AspNetCore.Mvc;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
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
            string token = _authService.Login(loginDto);
            
            return Ok(token);
        }

        [HttpGet("Test")]
        [Authorize]
        public ActionResult<string> Test()
        {
            return Ok("Token works");
        }
    }
}
