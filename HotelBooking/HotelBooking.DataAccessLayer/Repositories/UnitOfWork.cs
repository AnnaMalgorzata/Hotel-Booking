using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.DataAccessLayer.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly HotelContext _context;

    public UnitOfWork(HotelContext context)
    {
        _context = context;
    }

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
