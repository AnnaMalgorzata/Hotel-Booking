using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;

internal class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(DbContext context) : base(context)
    {
    }
}
