using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

/// <summary>
///     Variables are used for Edi Profile creation
/// </summary>
public class EdiVariable : Entity, IAggregateRoot
{
    public EdiVariable(Guid id, string title, string description, string placeholder)
    {
        Id = id;
        Title = title;
        Description = description;
        Placeholder = placeholder;
    }

    // Empty constructor for EF
    protected EdiVariable()
    {
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Placeholder { get; set; }
}