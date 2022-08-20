namespace city_traffic_simulator_backend.Repository;

using System.Linq.Expressions;
using MongoDB.Driver;

public interface IRepository<T>
{
    IQueryable<T> GetAll();

    Task<T> GetOneAsync(Expression<Func<T, bool>> predicate);

    Task InsertAsync(T obj);

    Task UpsertAsync(T obj);

    Task UpdateAsync(T obj);

    Task DeleteAsync(Expression<Func<T, bool>> predicate);
}