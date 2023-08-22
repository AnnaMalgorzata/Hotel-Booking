using HotelBooking.DataAccessLayer.Entities;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;
public interface IReservationRepository : IRepository<Reservation>
{
    public Task<IEnumerable<Reservation>> GetReservationsFromDateRange(DateTime from, DateTime to);
    public Task<string> GetGuestFirstname(Reservation reservation);
    public Task<string> GetGuestLastname(Reservation reservation);
}
