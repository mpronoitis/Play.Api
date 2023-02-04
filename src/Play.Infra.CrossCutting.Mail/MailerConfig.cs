using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Play.Infra.CrossCutting.Mail;

public static class MailerConfig
{
    /// <summary>
    ///     This code is defining a method AddMailerConfig that can be used to add email configuration to an
    ///     IServiceCollection.
    ///     The method takes an IConfiguration object as an argument, which is used to retrieve values from the EmailSettings
    ///     section of the configuration data.
    ///     These values include the hostname and port of an SMTP server, as well as a username and password for authenticating
    ///     with the server.
    ///     The method then uses these values to create an instance of SmtpClient with the specified credentials and SSL
    ///     setting.
    ///     Finally, the method adds a Fluent Email sender to the service collection using the AddSmtpSender method, passing in
    ///     the SmtpClient instance as an argument.
    ///     This allows the application to use the Fluent Email library to send emails through the specified SMTP server.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddMailerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //smtp credentials from EmailSettings section in app settings.json
        var emailSettings = configuration.GetSection("EmailSettings");
        var smtpServer = emailSettings.GetSection("Host").Value;
        var smtpPort = emailSettings.GetSection("Port").Value;
        var smtpUsername = emailSettings.GetSection("Username").Value;
        var smtpPassword = emailSettings.GetSection("Password").Value;

        //create SmtpClient
        Debug.Assert(smtpPort != null, nameof(smtpPort) + " != null");
        var smtpClient = new SmtpClient(smtpServer, int.Parse(smtpPort))
        {
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = false
        };

        services
            .AddFluentEmail("noreply@playsystems.io")
            .AddRazorRenderer()
            .AddSmtpSender(smtpClient);
    }
}