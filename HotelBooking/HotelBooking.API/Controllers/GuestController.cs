using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers;

[Route("guests")]
[ApiController]
public class GuestController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public GuestController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterGuest(RegistrationDto registrationDto)
    {
        await _registrationService.RegisterGuest(registrationDto);

        return Ok();
    }

}
