using MalwarebytesOneviewApi.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Play.Infra.CrossCutting.Mbam;

public static class MbamConfigurator
{
    /// <summary>
    ///     This method is intended to be used as part of a Dependency Injection setup in an ASP.NET Core application.
    ///     It adds the MbamOneViewApiConnector class to the service collection with a given set of configuration options.
    ///     The MbamOneViewApiConnector class is used to connect to the Malwarebytes OneView API, which is a service that
    ///     provides information about the status and configuration of Malwarebytes software products.
    ///     The configuration options are read from the Malwarebytes section of the appsettings.json file.
    ///     The ClientId, ClientSecret, and BaseUrl options are required, and an exception will be thrown if any of them is not
    ///     set.
    ///     The Scope option is also set to a fixed value of "read write execute".
    ///     Once this method has been called and the service collection has been built, an instance of the
    ///     MbamOneViewApiConnector class can be injected into any class that requires it.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddMbamConnector(this IServiceCollection services, IConfiguration configuration)
    {
        //get Malwarebytes section from appsettings.json
        var mbamSection = configuration.GetSection("Malwarebytes");
        services.AddMbamOneViewApiConnectorToCollection(options =>
        {
            options.Scope = "read write execute";
            options.ClientId = mbamSection["ClientId"] ??
                               throw new InvalidOperationException("Malwarebytes ClientId is not set");
            options.ClientSecret = mbamSection["ClientSecret"] ??
                                   throw new InvalidOperationException("Malwarebytes ClientSecret is required");
            options.BaseUrl = mbamSection["Endpoint"] ??
                              throw new InvalidOperationException("Malwarebytes BaseUrl is required");
        });
    }
}