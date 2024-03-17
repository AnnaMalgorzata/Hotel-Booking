using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;

internal class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(HotelContext context) : base(context)
    {
    }

    public async Task<Room> GetRoom(RoomType roomType, int capacity, DateTime startDate, DateTime endDate)
    {
        return await Context.Set<Room>()
            .Where(room => (room.Capacity == capacity && room.Type == roomType)
            &&
            !room.Reservations.Any(reservation =>
                (startDate >= reservation.DateFrom && startDate < reservation.DateTo) ||
                (endDate > reservation.DateFrom && endDate <= reservation.DateTo) ||
                (startDate < reservation.DateFrom && endDate > reservation.DateTo)
                ))
            .FirstOrDefaultAsync();
    }
}
