using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.BusinessLogic.Services.Implementation;
public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IValidationService<CreateReservationDto> _validationService;

    public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        //_validationService = validationService;
    }

    public async Task<Room> GetRoom(BasicRoomInfo basicRoomInfo, DateTime startDate, DateTime endDate)
    {
        return await _roomRepository.GetRoom((DataAccessLayer.Entities.RoomType)basicRoomInfo.Type, basicRoomInfo.Capacity, startDate, endDate);

    }

    /*public async Task<ReservationDto> AddRoom(RoomDto dto)
    {

        return dto;
    }*/
}
