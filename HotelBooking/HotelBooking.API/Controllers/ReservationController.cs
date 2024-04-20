using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(int id)
    {
        var todoItem = await _reservationService.GetReservation(id);

        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddReservation(CreateReservationDto reservation)
    {
        var id = await _reservationService.AddReservation(reservation);

        return Ok(id);
    }

}
