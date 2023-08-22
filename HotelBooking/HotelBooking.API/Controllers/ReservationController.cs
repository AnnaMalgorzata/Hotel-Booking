using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers;

[Route("reservations")]
public class ReservationController : ControllerBase
{
    private IReservationService _iReservationService;

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(int id)
    {
        var todoItem = await _iReservationService.GetReservation(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }

}
