using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;

internal class GuestRepository : Repository<Guest>, IGuestRepository
{
    public GuestRepository(HotelContext context) : base(context)
    {
    }

    public async Task<Guest> GetGuest(string email)
    {
        return await Context.Set<Guest>()
            .Where(x => x.Email == email)
            .SingleOrDefaultAsync();
    }
}
