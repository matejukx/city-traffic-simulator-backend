namespace city_traffic_simulator_backend;

using DependencyInjection;
using Entities;
using Repository;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(config => config.AddProfile(typeof(SimulationMapperProfile)));
        services.SetupMongo(this.Configuration);
    }

    public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
    {
        
    }
}