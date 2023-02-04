using NetDevPack.Domain;

namespace Play.Domain.Pylon.Models;

public class PylonContact : Entity, IAggregateRoot
{
    public PylonContact()
    {
    }

    public PylonContact(Guid id, Guid heid, string code, string name, string firstName, string lastName, string tin,
        string emails, string phones, string address, DateTime createdDate)
    {
        Id = id;
        Heid = heid;
        Code = code;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        Tin = tin;
        Emails = emails;
        Phones = phones;
        Address = address;
        CreatedDate = createdDate;
    }

    /// <summary>
    ///     Pylon heid (primary key)
    /// </summary>
    public Guid Heid { get; set; }

    /// <summary>
    ///     Code of the contact
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    ///     Name of the contact
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     First name of the contact
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    ///     Last name of the contact
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    ///     TIN of the contact
    /// </summary>
    public string Tin { get; set; } = null!;

    /// <summary>
    ///     Emails of the contact, separated by comma
    /// </summary>
    public string Emails { get; set; } = null!;

    /// <summary>
    ///     Phones of the contact, separated by comma
    /// </summary>
    public string Phones { get; set; } = null!;

    /// <summary>
    ///     Address of the contact
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Created date of the contact
    /// </summary>
    public DateTime CreatedDate { get; set; }
}