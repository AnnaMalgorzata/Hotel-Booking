using HotelBooking.DataAccessLayer.Entities;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;
public interface IReservationRepository : IRepository<Reservation>
{
    public Task<IEnumerable<Reservation>> GetReservationsFromDateRange(DateTime from, DateTime to);

    Task<Reservation> GetReservation(int id);
}
