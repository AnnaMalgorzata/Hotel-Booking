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

    public async Task<T> Get(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return Context.Set<T>();
    }

    public async Task Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return Context.Set<T>().Where(predicate);
    }


    public async Task Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public async Task RemoveRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }
}
