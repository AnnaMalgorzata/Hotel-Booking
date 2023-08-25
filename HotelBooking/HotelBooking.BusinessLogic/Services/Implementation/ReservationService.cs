using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.BusinessLogic.Services.Implementation;
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationDto> GetReservation(int id)
    {
        var reservation = await _reservationRepository.GetReservation(id);
        if (reservation is null)
        {
            throw new NotFoundException($"Reservation with Id = {id} does not exists.");
        }
        var roomInfos = new List<BasicRoomInfo>();

        roomInfos = reservation.Rooms.Select(c => new BasicRoomInfo(c.Type.ToString(), c.Capacity)).ToList();

        var dto = new ReservationDto()
        {
            Id = reservation.ReservationId,
            DateFrom = reservation.DateFrom,
            DateTo = reservation.DateTo,
            Price = reservation.Price,
            GuestFirstname = reservation.Guest.Lastname,
            GuestLastname = reservation.Guest.Firstname,
            RoomInfos = roomInfos
        };

        return dto;
    }
}
