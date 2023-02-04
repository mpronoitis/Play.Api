using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Whmcs.Connector.Core.Ioc;

namespace Play.Infra.CrossCutting.Whmcs;

public static class WhmcsConfigurator
{
    /// <summary>
    ///     The method AddWhmcs is a part of the service collection and is used to add the WhmcsConnector to the service
    ///     collection in the application's Dependency Injection (DI) container.
    ///     It takes an instance of IConfiguration as an input parameter and reads the Whmcs section from the configuration.
    ///     It then uses this configuration to set the Identifier, Secret, and Url options of the WhmcsConnector.
    ///     The Identifier and Secret are required options, and if they are not set, an InvalidOperationException is thrown.
    ///     The Url is also set to a default value of "https://playcloudservices.com/includes/api.php".
    ///     This method is typically called in the ConfigureServices method of the application's startup class, to add the
    ///     WhmcsConnector to the service collection, so that it can be used throughout the application.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddWhmcs(this IServiceCollection services, IConfiguration configuration)
    {
        //get Whmcs section from appsettings.json
        var whmcsSection = configuration.GetSection("Whmcs");
        services.AddWhmcsConnector(options =>
        {
            options.Identifier = whmcsSection["Identifier"] ??
                                 throw new InvalidOperationException("WHMCS Identifier is not set");
            options.Secret = whmcsSection["Secret"] ?? throw new InvalidOperationException("WHMCS Secret is not set");
            options.Url = "https://playcloudservices.com/includes/api.php";
        });
    }
}