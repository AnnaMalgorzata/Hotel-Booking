using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers;

[Route("guests")]
[ApiController]
public class GuestController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpPost]
    public async Task<ActionResult> AddGuest(GuestDto guestDto)
    {
        await _guestService.AddGuest(guestDto);

        return Ok();
    }

}
