namespace city_traffic_simulator_backend.Repository;

using Entities;
using MongoDB.Driver;

public class SettingsRepository : MongoDdRepository<Settings>
{
    public SettingsRepository(MongoContext context)
    {
        collection = context.Database.GetCollection<Settings>(nameof(Settings));  
        SetupIndex();
    }

    public override IEnumerable<Settings> ListAll()
    {
        var projection =
            Builders<Settings>.Projection.Expression(p => new Settings{
                Id = p.Id,
                Hash = p.Hash
            });
        return collection.Find(_ => true).Project(projection).ToEnumerable();
    }

    public override async Task UpsertAsync(Settings obj)
    {
        var existing = await GetOneAsync(d =>
            d.Hash == obj.Hash);
        if (existing is null)
        {
            await InsertAsync(obj);
            return;
        }
        await UpdateAsync(obj);
    }

    public override async Task UpdateAsync(Settings obj)
    { 
        var filter = Builders<Settings>.Filter
            .Where(d => d.Hash == obj.Hash);

        var updateDefinition = Builders<Settings>
            .Update
            .Set(d => d.Tags, obj.Tags);

        await collection.FindOneAndUpdateAsync(filter, updateDefinition);
    }

    private async Task SetupIndex()
    {
        var indexOptions = new CreateIndexOptions
        {
            Unique = true,
            Background = true
        };
        var indexKeys = Builders<Settings>.IndexKeys
            .Ascending(d => d.Hash);
        var indexModel = new CreateIndexModel<Settings>(indexKeys, indexOptions);
        await collection.Indexes.CreateOneAsync(indexModel);
    }
}