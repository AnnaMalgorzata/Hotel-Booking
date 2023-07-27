using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;

internal class GuestRepository : Repository<Guest>, IGuestRepository
{
    public GuestRepository(DbContext context) : base(context)
    {
    }
}
