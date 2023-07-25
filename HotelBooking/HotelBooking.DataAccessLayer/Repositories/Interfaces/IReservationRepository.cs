namespace HotelBooking.DataAccessLayer.Repositories;
public interface IReservationRepository : IRepository<Reservation>
{
    IEnumerable<Reservation> GetReservationsFromDateRange(DateTime from, DateTime to);
}
