using System;
using NetDevPack.Domain;

namespace Play.Domain.Core.Models;

public class EmailTemplate : Entity, IAggregateRoot
{
    public EmailTemplate(Guid id, string name, string subject, string body)
    {
        Id = id;
        Name = name;
        Subject = subject;
        Body = body;
        CreatedDate = DateTime.Now;
    }

    public EmailTemplate()
    {
    }

    /// <summary>
    ///     The name of the template
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    ///     The subject of the email
    /// </summary>
    public string Subject { get; protected set; }

    /// <summary>
    ///     The body of the email
    /// </summary>
    public string Body { get; protected set; }

    /// <summary>
    ///     The date the template was created
    /// </summary>
    public DateTime CreatedDate { get; protected set; }
}