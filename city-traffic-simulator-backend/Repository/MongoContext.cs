namespace city_traffic_simulator_backend.Repository;

using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoContext
{
    private readonly MongoClient _client;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        var settings = dbOptions.Value;
        _client = new MongoClient(settings.ConnectionString);
        Database = _client.GetDatabase(settings.DatabaseName);
    }

    public IMongoClient Client => _client;

    public IMongoDatabase Database { get; }
}