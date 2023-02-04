#nullable enable
using System.Collections.Generic;

namespace Play.Domain.Edi.Models;

/// <summary>
///     Document data Model is used for mapping received documents
///     Meant to be used when a document is being parsed to edi
/// </summary>
public class DocumentDataModel
{
    /// <summary>
    ///     Document date, the official document date
    /// </summary>
    public string? DATE { get; set; }

    /// <summary>
    ///     Document total tax ammount
    /// </summary>
    public string? TOTAL_TAX { get; set; }

    /// <summary>
    ///     Total count of items included in the document
    /// </summary>
    public string? ITEMS_COUNT { get; set; }

    /// <summary>
    ///     Total price of the document , tax included
    /// </summary>
    public string? TOTAL_PRICE { get; set; }

    /// <summary>
    ///     Total price of the document, tax excluded
    /// </summary>
    public string? TOTAL_PRICE_NO_TAX { get; set; }

    /// <summary>
    ///     Related document name, source document (transformed from)
    /// </summary>
    public string? SOURCE_DOCUMENT { get; set; }

    /// <summary>
    ///     Related document date,source document (transformed from)
    /// </summary>
    public string? SOURCE_DOCUMENT_DATE { get; set; }

    /// <summary>
    ///     Related document name,destination document {transformed to)
    /// </summary>
    public string? DESTINATION_DOCUMENT { get; set; }

    /// <summary>
    ///     Related document date,destination document (transformed to)
    /// </summary>
    public string? DESTINATION_DOCUMENT_DATE { get; set; }

    /// <summary>
    ///     All the items of the document
    /// </summary>
    public List<DocumentItemModel>? ITEMS { get; set; }
}