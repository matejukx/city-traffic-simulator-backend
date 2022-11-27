namespace city_traffic_simulator_backend.Repository;

using Entities;
using MongoDB.Driver;

public class SimulationDataRepository : MongoDdRepository<SimulationDataDocument>
{
    public SimulationDataRepository(MongoContext context)
    {
        this.collection = context.Database.GetCollection<SimulationDataDocument>(nameof(SimulationDataDocument));  
        SetupIndex();
    }

    public override IEnumerable<SimulationDataDocument> ListAll()
    {
        var projection =
            Builders<SimulationDataDocument>.Projection.Expression(p => new SimulationDataDocument{
                Id = p.Id,
                MapHash = p.MapHash,
                SettingsHash = p.SettingsHash,
                RunId = p.RunId
            });
        return collection.Find(_ => true).Project(projection).ToEnumerable();
    }

    public override async Task UpsertAsync(SimulationDataDocument obj)
    {
        var existing = await this.GetOneAsync(d =>
            d.MapHash == obj.MapHash && d.SettingsHash == obj.SettingsHash);
       if (existing is null)
       {
           await InsertAsync(obj);
           return;
       }
       await UpdateAsync(obj);
    }

    public override async Task UpdateAsync(SimulationDataDocument obj)
    { 
        var filter = Builders<SimulationDataDocument>.Filter
            .Where(d => d.MapHash == obj.MapHash && d.SettingsHash == obj.SettingsHash);

        var updateDefinition = Builders<SimulationDataDocument>
            .Update
            .PushEach(
                d => d.Frames, 
                obj.Frames);
        
        await collection.FindOneAndUpdateAsync(filter, updateDefinition);
    }

    private async Task SetupIndex()
    {
        var indexOptions = new CreateIndexOptions
        {
            Unique = true,
            Background = true
        };
        var indexKeys = Builders<SimulationDataDocument>.IndexKeys
            .Ascending(d => d.SettingsHash)
            .Ascending(d => d.MapHash);
        var indexModel = new CreateIndexModel<SimulationDataDocument>(indexKeys, indexOptions);
        await collection.Indexes.CreateOneAsync(indexModel);
    }
    
}