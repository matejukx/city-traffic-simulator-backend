namespace city_traffic_simulator_backend.Repository;

using Entities;
using MongoDB.Driver;

public class MapRepository : MongoDdRepository<Map>
{
    public MapRepository(MongoContext context)
    {
        collection = context.Database.GetCollection<Map>(nameof(Map));
        SetupIndex();
    }
    
    private async Task SetupIndex()
    {
        var indexOptions = new CreateIndexOptions
        {
            Unique = true,
            Background = true
        };
        var indexKeys = Builders<Map>.IndexKeys
            .Ascending(d => d.Hash);
        var indexModel = new CreateIndexModel<Map>(indexKeys, indexOptions);
        await collection.Indexes.CreateOneAsync(indexModel);
    }
}