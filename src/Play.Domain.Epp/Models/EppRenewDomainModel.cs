using NetDevPack.Domain;

namespace Play.Domain.Epp.Models;

public class EppRenewDomainModel : Entity, IAggregateRoot
{
    public EppRenewDomainModel(string domainName, int years)
    {
        DomainName = domainName;
        Years = years;
    }

    public EppRenewDomainModel()
    {
    }

    /// <summary>
    ///     The domain name to be renewed
    /// </summary>
    public string DomainName { get; set; } = null!;

    /// <summary>
    ///     The years to renew the domain for
    /// </summary>
    public int Years { get; set; }
}