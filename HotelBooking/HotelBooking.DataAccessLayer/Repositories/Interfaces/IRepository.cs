using HotelBooking.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;
public interface IRepository<T> where T : Entity
{
    T Get(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
