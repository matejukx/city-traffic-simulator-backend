namespace city_traffic_simulator_backend.Repository;

using System.Linq.Expressions;
using MongoDB.Driver;

public abstract class MongoDdRepository<T> : IRepository<T>
{
    internal IMongoCollection<T> collection;
    
    public IQueryable<T> GetAll()
    {
        return collection.AsQueryable();
    }

    public async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        return await collection.FindAsync(filter).Result.FirstOrDefaultAsync();
    }
    
    public async Task InsertAsync(T obj) 
    {
        await collection.InsertOneAsync(obj);
    }

    public virtual Task UpdateAsync(T obj)
    {
        throw new NotImplementedException();
    }

    public virtual Task UpsertAsync(T obj)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        await collection.DeleteOneAsync(predicate);
    }
}