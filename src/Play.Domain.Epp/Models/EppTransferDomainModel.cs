using NetDevPack.Domain;

namespace Play.Domain.Epp.Models;

public class EppTransferDomainModel : Entity, IAggregateRoot
{
    public EppTransferDomainModel(string domainName, string password, string newPassword, string contactId)
    {
        DomainName = domainName;
        Password = password;
        NewPassword = newPassword;
        ContactId = contactId;
    }

    public EppTransferDomainModel()
    {
    }

    /// <summary>
    ///     The domain name
    /// </summary>
    public string DomainName { get; set; } = null!;

    /// <summary>
    ///     The password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    ///     The new password
    /// </summary>
    public string NewPassword { get; set; } = null!;

    /// <summary>
    ///     The contact id
    /// </summary>
    public string ContactId { get; set; } = null!;
}