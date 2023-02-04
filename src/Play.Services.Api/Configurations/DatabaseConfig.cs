namespace Play.Services.Api.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<PlayContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<PlayCoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CoreConnection")));

        services.AddDbContext<PlayPylonContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PylonTempDatabase")));
        
        services.AddDbContext<PlayEventStoreContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("EventSourcing")));
    }
}