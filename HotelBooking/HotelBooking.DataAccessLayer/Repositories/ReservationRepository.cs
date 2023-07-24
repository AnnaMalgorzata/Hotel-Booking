using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;
internal class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(DbContext context) : base(context)
    { }

    public IEnumerable<Reservation> GetReservationsFromDateRange(DateTime from, DateTime to)
    {
        return Context.Set<Reservation>().Where(r => DateTime.Compare(r.DateFrom, from) >=0 && DateTime.Compare(r.DateTo, to) <=0).OrderByDescending(r => r.ReservationId);
    }
}
