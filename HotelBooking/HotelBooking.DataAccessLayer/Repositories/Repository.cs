using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelBooking.DataAccessLayer.Repositories;
internal class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        Context = context;
    }

    public T Get(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>();
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return Context.Set<T>().Where(predicate);
    }


    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }
}
