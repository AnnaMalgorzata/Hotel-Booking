using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAccessLayer.Repositories;
internal interface IReservationRepository : IRepository<Reservation>
{
    IEnumerable<Reservation> GetReservationsFromDateRange(DateTime from, DateTime to);
}
