namespace city_traffic_simulator_backend.Repository;

using Entities;
using MongoDB.Driver;

public class SimulationDataRepository : MongoDdRepository<SimulationData>
{
    public SimulationDataRepository(MongoContext context) : base(context)
    {
    }
    
    public override async Task<SimulationData> UpdateAsync(SimulationData obj)
    { 
        var filter = Builders<SimulationData>.Filter.Where(x => x.Id == obj.Id);
        
        var updateDefBuilder = Builders<SimulationData>.Update;
        var updateDef = updateDefBuilder.Combine(new UpdateDefinition<SimulationData>[]
        {
            updateDefBuilder.Set(x => x.AddedOn, obj.AddedOn)
        });
        return await collection.FindOneAndUpdateAsync(filter, updateDef);
    }
}