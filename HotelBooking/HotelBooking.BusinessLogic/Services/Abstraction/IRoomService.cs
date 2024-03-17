using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.DataAccessLayer.Entities;

namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IRoomService
{
    public Task<Room> GetRoom(BasicRoomInfo basicRoomInfo, DateTime startDate, DateTime endDate);
}
