using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Epp.Connector.Ioc;

namespace Play.Infra.CrossCutting.Epp;

// (Extensible Provisioning Protocol) connector to a service collection in the dependency injection container of an ASP.NET Core application.
// It takes an instance of the IConfiguration interface as an argument, which is used to retrieve the EPP configuration section from the appsettings.json file.
//  The method then adds the EPP connector to the service collection by calling the AddEppConnectorToCollection method and passing a lambda expression that configures the options for the EPP connector.
// The lambda expression retrieves the EPP endpoint, username, and password from the configuration section and sets them as options for the EPP connector.
// If any of these values are not found in the configuration, an exception is thrown with an appropriate error message.
//The EPP connector is a service that is used to communicate with an EPP server using the EPP protocol.
//It allows the application to perform various EPP-related tasks, such as managing domain names, checking domain availability, and transferring domains.
public static class EppConfigurator
{
    public static void AddEppConnector(this IServiceCollection services, IConfiguration configuration)
    {
        //get Epp section from appsettings.json
        var eppSection = configuration.GetSection("Epp");
        services.AddEppConnectorToCollection(options =>
        {
            options.EppEndpoint = eppSection["Endpoint"] ??
                                  throw new InvalidOperationException("EPP Endpoint is not configured");
            options.EppUsername = eppSection["Username"] ??
                                  throw new InvalidOperationException("EPP Username is not configured");
            options.EppPassword = eppSection["Password"] ??
                                  throw new InvalidOperationException("EPP Password is not configured");
        });
    }
}