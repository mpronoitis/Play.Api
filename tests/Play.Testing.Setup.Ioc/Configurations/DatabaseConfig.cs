using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Infra.Data.Context;

namespace Play.Testing.Ioc.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<PlayContext>(options =>
            options.UseInMemoryDatabase("Play"));

        services.AddDbContext<PlayCoreContext>(options =>
            options.UseInMemoryDatabase("PlayCore"));

        services.AddDbContext<PlayPylonContext>(options =>
            options.UseInMemoryDatabase("PlayPylon"));
    }
}