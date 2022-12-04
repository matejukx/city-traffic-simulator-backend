namespace city_traffic_simulator_backend.Repository;

using Entities;
using MongoDB.Driver;

public class TagsRepository : MongoDdRepository<TagDocument>
{
    public TagsRepository(MongoContext context)
    {
        collection = context.Database.GetCollection<TagDocument>(nameof(TagDocument));
        SetupIndex();
    }
    
    private async Task SetupIndex()
    {
        var indexOptions = new CreateIndexOptions
        {
            Unique = true,
            Background = true
        };
        var indexKeys = Builders<TagDocument>.IndexKeys
            .Ascending(d => d.Name);
        var indexModel = new CreateIndexModel<TagDocument>(indexKeys, indexOptions);
        await collection.Indexes.CreateOneAsync(indexModel);
    }
}