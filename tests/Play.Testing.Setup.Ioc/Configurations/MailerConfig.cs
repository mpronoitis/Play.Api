using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Play.Testing.Ioc.Configurations;

public static class MailerConfig
{
    public static void AddMailerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //smtp credentials from EmailSettings section in app settings.json
        var emailSettings = configuration.GetSection("EmailSettings");
        var smtpServer = emailSettings.GetSection("Host").Value;
        var smtpPort = emailSettings.GetSection("Port").Value;
        var smtpUsername = emailSettings.GetSection("Username").Value;
        var smtpPassword = emailSettings.GetSection("Password").Value;

        //create SmtpClient
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