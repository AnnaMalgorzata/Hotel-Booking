using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.BusinessLogic.Services.Implementation;
public class ReservationService : IReservationService
{
    private IReservationRepository _iReservationRepository;

    public ReservationService(IReservationRepository iReservationRepository)
    {
        _iReservationRepository = iReservationRepository;
    }

    public async Task<ReservationDto> GetReservation(int id)
    {
        var reservation = await _iReservationRepository.Get(id);
        var roomInfos = new List<BasicRoomInfo>();

        roomInfos = reservation.Rooms.Select(c => new BasicRoomInfo(c.Type.ToString(), c.Capacity)).ToList();

        var dto = new ReservationDto()
        {
            Id = reservation.ReservationId,
            DateFrom = reservation.DateFrom,
            DateTo = reservation.DateTo,
            Price = reservation.Price,
            GuestFirstname = await _iReservationRepository.GetGuestFirstname(reservation),
            GuestLastname = await _iReservationRepository.GetGuestLastname(reservation),
            RoomInfos = roomInfos
        };

        return dto;
    }
}
