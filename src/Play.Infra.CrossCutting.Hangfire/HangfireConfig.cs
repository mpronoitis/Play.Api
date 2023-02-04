using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Play.Infra.CrossCutting.Hangfire;

public static class HangfireConfig
{
    public static void AddHangfireConfig(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        //get hangfire connection string
        var hangfireConnectionString = configuration.GetConnectionString("HangfireDB");

        //The SetDataCompatibilityLevel method is used to set the data compatibility level for Hangfire. This determines which version of Hangfire the data stored in the database is compatible with.
        //The UseSimpleAssemblyNameTypeSerializer method specifies that Hangfire should use the SimpleAssemblyNameTypeSerializer class to serialize and deserialize types when storing them in the database.
        //The UseRecommendedSerializerSettings method specifies that Hangfire should use the recommended serialization settings when serializing and deserializing objects.
        //The UseSqlServerStorage method specifies that Hangfire should use the SQL Server storage provider to store its data in the database.
        //The CommandBatchMaxTimeout property sets the maximum time allowed for a batch of commands sent to the database. The SlidingInvisibilityTimeout property sets the sliding invisibility timeout for processed jobs. The QueuePollInterval property sets the interval at which Hangfire polls the queues for new jobs.
        //The UseRecommendedIsolationLevel property specifies that Hangfire should use the recommended isolation level for SQL Server. The DisableGlobalLocks property disables global locks
        services.AddHangfire(globalConfiguration => globalConfiguration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(hangfireConnectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        // Add the processing server as IHostedService
        services.AddHangfireServer();
    }
}