namespace city_traffic_simulator_backend;

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
        
        services.Configure<DatabaseSettings>(
            Configuration.GetSection("MongoConnection"));
        services.AddSingleton<MongoContext>();
    }

    public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
    {
        
    }
}