﻿using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace HotelBooking.DataAccessLayer.Repositories;
public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly HotelContext Context;

    public Repository(HotelContext context)
    {
        Context = context;
    }

    public async Task<T> Get(int id)
    {
        return await Context.Set<T>().FindAsync(id); 
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return Context.Set<T>();
    }

    public void Add(T entity)
    {
        Context.Set<T>().AddAsync(entity);
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
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
