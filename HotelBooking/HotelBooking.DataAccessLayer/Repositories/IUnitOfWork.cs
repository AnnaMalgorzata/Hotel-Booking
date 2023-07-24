using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAccessLayer.Repositories;
internal interface IUnitOfWork : IDisposable
{
    IReservationRepository ReservationRepository { get; }
    int Commit();
}
