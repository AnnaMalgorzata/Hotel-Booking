namespace HotelBooking.DataAccessLayer.Repositories;
public interface IUnitOfWork : IDisposable
{
    int Commit();
}
