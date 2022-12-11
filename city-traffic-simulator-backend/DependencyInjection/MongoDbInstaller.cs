namespace city_traffic_simulator_backend.DependencyInjection;

using Entities;
using Repository;

public static class MongoDbInstaller
{
    public static void SetupMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(
            configuration.GetSection("MongoConnection"));
        services.AddScoped<MongoContext>();
        services.AddScoped<IRepository<SimulationDataDocument>,SimulationDataRepository>();
        services.AddScoped<IRepository<Settings>, SettingsRepository>();
        services.AddScoped<IRepository<TagDocument>, TagsRepository>();
        services.AddScoped<IRepository<Map>, MapRepository>();
    }
}