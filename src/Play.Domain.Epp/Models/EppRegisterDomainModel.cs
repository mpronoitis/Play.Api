using NetDevPack.Domain;

namespace Play.Domain.Epp.Models;

public class EppRegisterDomainModel : Entity, IAggregateRoot
{
    /// <summary>
    ///     Constructor with parameters.
    /// </summary>
    /// <param name="domainName">The domain name to register.</param>
    /// <param name="registrant">The registrant contact.</param>
    /// <param name="admin">The admin contact.</param>
    /// <param name="tech">The tech contact.</param>
    /// <param name="billing">The billing contact.</param>
    /// <param name="password">The password for the domain.</param>
    /// <param name="period">The period in years.</param>
    /// <returns></returns>
    public EppRegisterDomainModel(string domainName, string registrant, string admin, string tech, string billing,
        string password, int period)
    {
        DomainName = domainName;
        Registrant = registrant;
        Admin = admin;
        Tech = tech;
        Billing = billing;
        Password = password;
        Period = period;
    }

    //empty constructor
    protected EppRegisterDomainModel()
    {
    }

    /// <summary>
    ///     The domain name to register.
    /// </summary>
    public string DomainName { get; set; } = null!;

    /// <summary>
    ///     The registrant contact.
    /// </summary>
    public string Registrant { get; set; } = null!;

    /// <summary>
    ///     The admin contact.
    /// </summary>
    public string Admin { get; set; } = null!;

    /// <summary>
    ///     The tech contact.
    /// </summary>
    public string Tech { get; set; } = null!;

    /// <summary>
    ///     The billing contact.
    /// </summary>
    public string Billing { get; set; } = null!;

    /// <summary>
    ///     The password for the domain.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    ///     The period in years.
    /// </summary>
    public int Period { get; set; }
}