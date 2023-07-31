using HotelBooking.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;
public interface IRepository<T> where T : Entity
{
    Task<T> Get(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    Task Add(T entity);
    Task Remove(T entity);
    Task RemoveRange(IEnumerable<T> entities);
}
