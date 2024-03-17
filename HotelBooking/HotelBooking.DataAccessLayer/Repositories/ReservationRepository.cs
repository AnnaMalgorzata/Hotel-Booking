using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;
internal class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(HotelContext context) : base(context)
    { }

    public async Task<IEnumerable<Reservation>> GetReservationsFromDateRange(DateTime from, DateTime to)
    {
        return Context.Set<Reservation>().Where(r => DateTime.Compare(r.DateFrom, from) >=0 && DateTime.Compare(r.DateTo, to) <=0).OrderByDescending(r => r.ReservationId);
    }

    public async Task<Reservation> GetReservation(int id)
    {
        return await Context.Set<Reservation>()
            .Where(x => x.ReservationId == id)
            .Include(x => x.Rooms)
            .Include(x => x.Guest)
            .SingleOrDefaultAsync();
    }

    public async Task AddReservation(Reservation reservation)
    {
        await Context.Set<Reservation>()
            .AddAsync(reservation);
    }


}