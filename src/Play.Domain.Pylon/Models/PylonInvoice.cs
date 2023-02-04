using NetDevPack.Domain;

namespace Play.Domain.Pylon.Models;

public class PylonInvoice : Entity, IAggregateRoot
{
    /// <summary>
    ///     Empty constructor for ef
    /// </summary>
    public PylonInvoice()
    {
    }

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="invoiceNumber"></param>
    /// <param name="invoiceCode"></param>
    /// <param name="invoiceDate"></param>
    /// <param name="paymentMethod"></param>
    /// <param name="totalAmountNoTax"></param>
    /// <param name="totalAmountWithTax"></param>
    /// <param name="totalVat"></param>
    /// <param name="customerTin"></param>
    /// <param name="customerName"></param>
    /// <param name="vatRegime"></param>
    /// <param name="eipUrl"></param>
    /// <param name="invoiceLines"></param>
    public PylonInvoice(Guid id, string invoiceNumber, string invoiceCode, DateTime invoiceDate, string paymentMethod,
        decimal totalAmountNoTax, decimal totalAmountWithTax, decimal totalVat, string customerTin, string customerName,
        string vatRegime, string eipUrl, List<PylonInvoiceLine> invoiceLines)
    {
        Id = id;
        InvoiceNumber = invoiceNumber;
        InvoiceCode = invoiceCode;
        InvoiceDate = invoiceDate;
        PaymentMethod = paymentMethod;
        TotalAmountNoTax = totalAmountNoTax;
        TotalAmountWithTax = totalAmountWithTax;
        TotalVat = totalVat;
        CustomerTin = customerTin;
        CustomerName = customerName;
        VatRegime = vatRegime;
        EipUrl = eipUrl;
        InvoiceLines = invoiceLines;
    }

    /// <summary>
    ///     The A/A number of the invoice.
    /// </summary>
    public string InvoiceNumber { get; set; } = null!;

    /// <summary>
    ///     The invoice code (e.x. ΤΙΠ-52121421421)
    /// </summary>
    public string InvoiceCode { get; set; } = null!;

    /// <summary>
    ///     The date the invoice was created.
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    ///     The payment method used for the invoice.
    /// </summary>
    public string PaymentMethod { get; set; } = null!;

    /// <summary>
    ///     The total ammount of the invoice w/o VAT.
    /// </summary>
    public decimal TotalAmountNoTax { get; set; }

    /// <summary>
    ///     The total ammount of the invoice w/ VAT.
    /// </summary>
    public decimal TotalAmountWithTax { get; set; }

    /// <summary>
    ///     The total VAT of the invoice.
    /// </summary>
    public decimal TotalVat { get; set; }

    /// <summary>
    ///     The TIN of the customer.
    /// </summary>
    public string CustomerTin { get; set; } = null!;

    /// <summary>
    ///     The name of the customer.
    /// </summary>
    public string CustomerName { get; set; } = null!;

    /// <summary>
    ///     Vat Regime
    /// </summary>
    public string VatRegime { get; set; } = null!;

    /// <summary>
    ///     EIP url
    /// </summary>
    public string EipUrl { get; set; } = null!;

    /// <summary>
    ///     Invoice lines
    /// </summary>
    public List<PylonInvoiceLine> InvoiceLines { get; set; } = null!;
}