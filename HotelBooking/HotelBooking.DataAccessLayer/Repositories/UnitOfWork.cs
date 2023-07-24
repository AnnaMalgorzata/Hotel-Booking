using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAccessLayer.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    public IReservationRepository ReservationRepository { get; private set; }

    public UnitOfWork(DbContext context)
    {
        _context = context;
        ReservationRepository = new ReservationRepository(_context);
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
