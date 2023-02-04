using NetDevPack.Domain;

namespace Play.Domain.Pylon.Models;

/// <summary>
///     A pylon invoice line is a line on a pylon invoice and a sub type of PylonInvoice.
/// </summary>
public class PylonInvoiceLine : Entity, IAggregateRoot
{
    /// <summary>
    ///     Empty constructor for EF.
    /// </summary>
    public PylonInvoiceLine()
    {
    }

    /// <summary>
    ///     Constructor for creating a new invoice line.
    /// </summary>
    /// <param name="id">The id of the invoice line.</param>
    /// <param name="itemCode">The item code of the invoice line.</param>
    /// <param name="itemName">The item name of the invoice line.</param>
    /// <param name="itemDescription">The item description of the invoice line.</param>
    /// <param name="quantity">The quantity of the invoice line.</param>
    /// <param name="unitPrice">The unit price of the invoice line.</param>
    /// <param name="vatRate">The VAT rate of the invoice line.</param>
    /// <param name="totalPrice">The total price of the invoice line.</param>
    /// <param name="totalVat">The total VAT of the invoice line.</param>
    /// <param name="totalPriceWithVat">The total price of the invoice line (w VAT).</param>
    /// <param name="measurementUnit">The measurement unit of the invoice line.</param>
    public PylonInvoiceLine(Guid id, string itemCode, string itemName, string itemDescription, decimal quantity,
        decimal unitPrice, decimal vatRate, decimal totalPrice, decimal totalVat, decimal totalPriceWithVat,
        string measurementUnit)
    {
        Id = id;
        ItemCode = itemCode;
        ItemName = itemName;
        ItemDescription = itemDescription;
        Quantity = quantity;
        UnitPrice = unitPrice;
        VatRate = vatRate;
        TotalPrice = totalPrice;
        TotalVat = totalVat;
        TotalPriceWithVat = totalPriceWithVat;
        MeasurementUnit = measurementUnit;
    }

    /// <summary>
    ///     The item code
    /// </summary>
    public string ItemCode { get; set; } = null!;

    /// <summary>
    ///     The item name of the invoice line.
    /// </summary>
    public string ItemName { get; set; } = null!;

    /// <summary>
    ///     The item description of the invoice line.
    /// </summary>
    public string ItemDescription { get; set; } = null!;

    /// <summary>
    ///     The Quantity of the invoice line.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    ///     The unit price of the invoice line (w/o VAT).
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///     The VAT rate of the invoice line.
    /// </summary>
    public decimal VatRate { get; set; }

    /// <summary>
    ///     The total price of the invoice line (w/o VAT).
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    ///     The total VAT of the invoice line.
    /// </summary>
    public decimal TotalVat { get; set; }

    /// <summary>
    ///     The total price of the invoice line (w VAT).
    /// </summary>
    public decimal TotalPriceWithVat { get; set; }

    /// <summary>
    ///     The measurement unit of the invoice line.
    /// </summary>
    public string MeasurementUnit { get; set; } = null!;
}