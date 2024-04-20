using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAccessLayer.Repositories;
internal class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(HotelContext context) : base(context)
    { }

    public async Task<IEnumerable<Reservation>> GetReservationsFromDateRange(DateOnly from, DateOnly to)
    {
        return Context.Set<Reservation>().Where(r => from.CompareTo(r.DateFrom) <=0 && to.CompareTo(r.DateTo) <=0).OrderByDescending(r => r.ReservationId);
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