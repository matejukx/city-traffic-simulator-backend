namespace city_traffic_simulator_backend.Repository;

using System.Linq.Expressions;
using MongoDB.Driver;

public abstract class MongoDdRepository<T> : IRepository<T>
{
    internal IMongoCollection<T> collection;

    public MongoDdRepository(MongoContext context)
    {
        this.collection = context.Database.GetCollection<T>(nameof(T));
    }
    
    public IQueryable<T> GetAll()
    {
        return collection.AsQueryable();
    }

    public async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        return (await collection.FindAsync(filter)).FirstOrDefault();
    }

    public async Task InsertAsync(T obj)
    {
        await collection.InsertOneAsync(obj);
    }

    public virtual Task<T> UpdateAsync(T obj)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        await collection.DeleteOneAsync(predicate);
    }
}