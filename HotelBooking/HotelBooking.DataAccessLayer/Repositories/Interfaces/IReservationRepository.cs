using HotelBooking.DataAccessLayer.Entities;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;
public interface IReservationRepository : IRepository<Reservation>
{
    public Task<IEnumerable<Reservation>> GetReservationsFromDateRange(DateOnly from, DateOnly to);

    Task<Reservation> GetReservation(int id);

    Task AddReservation(Reservation reservation);
}
