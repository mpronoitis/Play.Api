using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

/// <summary>
///     An edi organization represents a party that is receiving edi messages.
/// </summary>
public class EdiOrganization : Entity, IAggregateRoot
{
    /// <summary>
    ///     Constructor used by commands , includes the Entity Id
    /// </summary>
    public EdiOrganization(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    // Empty constructor for EF
    public EdiOrganization()
    {
    }

    /// <summary>
    ///     Name of the organization.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Email address of the organization.
    /// </summary>
    public string Email { get; set; }
}