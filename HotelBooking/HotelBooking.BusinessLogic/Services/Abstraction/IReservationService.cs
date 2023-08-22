using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IReservationService
{
    public Task<ReservationDto> GetReservation(int id);
}
