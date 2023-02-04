#nullable enable
namespace Play.Domain.Edi.Models;

public class DocumentItemModel
{
    /// <summary>
    ///     The item index
    /// </summary>
    public int? ID { get; set; }

    /// <summary>
    ///     The unique item code
    /// </summary>
    public string? CODE { get; set; }

    /// <summary>
    ///     The item description
    /// </summary>
    public string? NAME { get; set; }

    /// <summary>
    ///     The item barcode
    /// </summary>
    public string? BARCODE { get; set; }

    /// <summary>
    ///     The item ammount
    /// </summary>
    public string? QUANTITY { get; set; }

    /// <summary>
    ///     The total item line tax ammount
    /// </summary>
    public string? TAX_VALUE { get; set; }

    /// <summary>
    ///     The item piece price
    /// </summary>
    public string? PIECE_PRICE { get; set; }

    /// <summary>
    ///     The item tax %
    ///     example: 13,24,0
    /// </summary>
    public string? TAX_PERCENT { get; set; }

    /// <summary>
    ///     The total item line price , tax exempt
    /// </summary>
    public string? PRICE_NO_TAX { get; set; }

    /// <summary>
    ///     The total item line price, tax included
    /// </summary>
    public string? PRICE_WITH_TAX { get; set; }

    /// <summary>
    ///     The item measurment unit
    /// </summary>
    public string? MEASURMENT_UNIT { get; set; }
}