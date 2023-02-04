using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Testing.Ioc;
using Play.Testing.Ioc.Configurations;

namespace Play.Testing.Setup.Runner;

public static class SetupRunner
{
    public static ServiceProvider Setup()
    {
        //create service collection
        var services = new ServiceCollection();

        //load configuration from appsettings.Development.json
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();


        //add db config
        services.AddDatabaseConfiguration(config);

        //add automapper
        services.AddAutoMapperConfiguration();

        //add mailer
        services.AddMailerConfig(config);

        //mediatr bus
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        //add Bootstraper services
        services.AddServices();


        return services.BuildServiceProvider();
    }
}