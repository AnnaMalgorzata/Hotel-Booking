using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.DataAccessLayer.Repositories;

internal class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(HotelContext context) : base(context)
    {
    }
}
