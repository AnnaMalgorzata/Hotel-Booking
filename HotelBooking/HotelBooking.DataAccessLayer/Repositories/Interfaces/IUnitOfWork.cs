namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;
public interface IUnitOfWork : IDisposable
{
    int Commit();
}
