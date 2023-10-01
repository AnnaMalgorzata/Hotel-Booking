
using HotelBooking.DataAccessLayer.Entities;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;

public interface IGuestRepository : IRepository<Guest>
{
    Task<Guest> GetGuest(string email);
}
