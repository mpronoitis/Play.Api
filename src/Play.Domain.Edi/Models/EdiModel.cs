using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

public class EdiModel : Entity, IAggregateRoot
{
    /// <summary>
    ///     Constructor to be used when creating a new model via mediator
    /// </summary>
    /// <param name="id"></param>
    /// <param name="org_id"></param>
    /// <param name="title"></param>
    /// <param name="segmentTerminator"></param>
    /// <param name="subElementSeparator"></param>
    /// <param name="elementSeparator"></param>
    /// <param name="enabled"></param>
    public EdiModel(Guid id, Guid org_id, string title, char segmentTerminator, char subElementSeparator,
        char elementSeparator, bool enabled)
    {
        Id = id;
        Org_Id = org_id;
        Title = title;
        SegmentTerminator = segmentTerminator;
        SubElementSeparator = subElementSeparator;
        ElementSeparator = elementSeparator;
        Enabled = enabled;
    }


    //empty constructor for ef
    public EdiModel()
    {
    }

    /// <summary>
    ///     Id of the organization that this model belongs to
    /// </summary>
    public Guid Org_Id { get; set; }

    /// <summary>
    ///     Title of the model
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Segment terminator for given model
    /// </summary>
    public char SegmentTerminator { get; set; }

    /// <summary>
    ///     Sub element separator for given model
    /// </summary>
    public char SubElementSeparator { get; set; }

    /// <summary>
    ///     Element separator for given model
    /// </summary>
    public char ElementSeparator { get; set; }

    /// <summary>
    ///     Enabled flag
    /// </summary>
    public bool Enabled { get; set; }
}