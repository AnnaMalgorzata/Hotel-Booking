using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.DataAccessLayer.Repositories;

internal class GuestRepository : Repository<Guest>, IGuestRepository
{
    public GuestRepository(HotelContext context) : base(context)
    {
    }
}
