//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MongoDbInstaller.cs" company="automotiveMastermind">
//    © automotiveMastermind. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
    }
}