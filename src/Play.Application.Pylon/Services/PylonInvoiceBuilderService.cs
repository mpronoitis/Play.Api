using Microsoft.Extensions.Logging;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Core.Interfaces;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using PylonDatabaseHandler.models.pylon;

namespace Play.Application.Pylon.Services;

/// <summary>
///     Service for building <see cref="PylonInvoice" />.
/// </summary>
public class PylonInvoiceBuilderService : IPylonInvoiceBuilderService
{
    private readonly ILogger _logger;
    private readonly IPylonCentlineRepository _pylonCentlineRepository;
    private readonly IPylonCommercialEntriesRepository _pylonCommercialEntriesRepository;
    private readonly IPylonCustomerRepository _pylonCustomerRepository;
    private readonly IPylonDentEipInfoRepository _pylonDentEipInfoRepository;
    private readonly IPylonDocentriesRepository _pylonDocentriesRepository;
    private readonly IPylonHeItemRepository _pylonHeItemRepository;
    private readonly IPylonPaymentMethodRepository _pylonPaymentMethodRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public PylonInvoiceBuilderService(IUserProfileRepository userProfileRepository,
        IPylonCommercialEntriesRepository pylonCommercialEntriesRepository,
        IPylonDocentriesRepository pylonDocentriesRepository, IPylonCentlineRepository pylonCentlineRepository,
        IPylonPaymentMethodRepository pylonPaymentMethodRepository, IPylonCustomerRepository pylonCustomerRepository,
        IPylonDentEipInfoRepository pylonDentEipInfoRepository, IPylonHeItemRepository pylonHeItemRepository,
        ILogger<PylonInvoiceBuilderService> logger)
    {
        _userProfileRepository = userProfileRepository;
        _pylonCommercialEntriesRepository = pylonCommercialEntriesRepository;
        _pylonDocentriesRepository = pylonDocentriesRepository;
        _pylonCentlineRepository = pylonCentlineRepository;
        _pylonPaymentMethodRepository = pylonPaymentMethodRepository;
        _pylonCustomerRepository = pylonCustomerRepository;
        _pylonDentEipInfoRepository = pylonDentEipInfoRepository;
        _pylonHeItemRepository = pylonHeItemRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Builds all available <see cref="PylonInvoice" /> for the given <paramref name="userId" />.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns> A collection of <see cref="PylonInvoice" />.</returns>
    public async Task<IEnumerable<PylonInvoice>> BuildAll(Guid userId)
    {
        //get user profile based on the given user id
        var userProfile = await _userProfileRepository.GetByUserId(userId);
        //return empty collection if user profile is not found
        if (userProfile == null || userProfile.TIN is null or "" or "0")
            return Enumerable.Empty<PylonInvoice>();

        //log beginning of the process
        _logger.LogInformation("Building all invoices for user {userProfile}", userProfile);

        //get all commercial entries from pylon for the given TIN
        var commercialEntries = await _pylonCommercialEntriesRepository.GetCommercialEntriesByTinAsync(userProfile.TIN);

        //create a new collection for commercialEntries that have the desired series id
        var filteredCommercialEntries = new List<Hecommercialentries>();
        //create a new collection for the matching doc entries for the filtered commercial entries
        var filteredDocEntries = new List<Hedocentries>();

        //loop commercial entries and keep only the ones that have a document entry with Hedcsrid equal 28b01403-cb0c-ec11-868c-001a7dda7106 (ΠΑΡΑΣΤΑΤΙΚΟ ΠΩΛΗΣΗΣ PCS)
        foreach (var commercialEntry in commercialEntries)
        {
            var documentEntry = await _pylonDocentriesRepository.GetDocentryByHeidAsync(commercialEntry.Hedentid);
            //if document entry is not found, continue to the next commercial entry
            if (documentEntry is null)
                continue;
            if (documentEntry.Hedcsrid != new Guid("28b01403-cb0c-ec11-868c-001a7dda7106".ToUpper()) && //tpy PCS
                documentEntry.Hedcsrid != new Guid("28b01403-cb0c-ec11-868c-001a7dda7106".ToUpper()) && //tip PCS
                documentEntry.Hedcsrid != new Guid("a49d3e29-5dea-e811-960b-0023245c6112".ToUpper()) && //TYP
                documentEntry.Hedcsrid != new Guid("809d3e29-5dea-e811-960b-0023245c6112".ToUpper())) //TIP
                continue;
            filteredCommercialEntries.Add(commercialEntry);
            filteredDocEntries.Add(documentEntry);
        }

        //create a new collection for the invoices
        var invoices = new List<PylonInvoice>();

        //loop filtered commercial entries and create a new invoice for each one
        for (var i = 0; i < filteredCommercialEntries.Count; i++)
        {
            var commercialEntry = filteredCommercialEntries[i];
            var documentEntry = filteredDocEntries[i];

            //get all the cent-lines for the given commercial entry
            var centlines = await _pylonCentlineRepository.GetCentlinesByHedentidAsync(commercialEntry.Hedentid);
            //get invoice data
            var paymentMethod = await _pylonPaymentMethodRepository.GetPaymentMethodByHeid(commercialEntry.Hepmmtid);
            var totalPrice = commercialEntry.Hettotalval;
            var totalTax = commercialEntry.Hetvatval;
            var customer = commercialEntry.Hebillcstmid != null
                ? await _pylonCustomerRepository.GetCustomerByHeidAsync((Guid)commercialEntry.Hebillcstmid)
                : null;
            var customerName = customer != null ? customer.Hename : "N/A";
            var eipUrl = _pylonDentEipInfoRepository.GetHeqrcodeByDentid(documentEntry.Heid);
            //create a new invoice
            var invoice = new PylonInvoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = documentEntry.Hedocnum?.ToString() ?? "N/A",
                InvoiceCode = documentEntry.Hedoccode ?? "N/A",
                InvoiceDate = documentEntry.Heofficialdate ?? DateTime.MinValue,
                PaymentMethod = paymentMethod?.Hename ?? "N/A",
                TotalAmountNoTax = totalPrice - totalTax,
                TotalAmountWithTax = totalPrice,
                TotalVat = totalTax,
                CustomerTin = commercialEntry.Hetin ?? "N/A",
                CustomerName = customerName,
                VatRegime = "κανονικός",
                EipUrl = eipUrl ?? "N/A"
            };
            //add invoice lines
            var invoiceLines = new List<PylonInvoiceLine>();

            //loop centlines and create a new invoice line for each one
            foreach (var centline in centlines)
            {
                //get data
                var item = await _pylonHeItemRepository.GetHeitemByHeid(centline.Heitemid);

                var invoiceLine = new PylonInvoiceLine
                {
                    Id = Guid.NewGuid(),
                    ItemCode = item?.Hecode ?? "N/A",
                    ItemName = item?.Hename ?? "N/A",
                    ItemDescription = CutString(item?.Hedetaileddescr ?? "N/A", 99),
                    Quantity = centline.Heaqty,
                    UnitPrice = centline.Heprice,
                    VatRate = centline.Hevatpercent,
                    TotalPrice = centline.Hettotalval - centline.Hetvatval,
                    TotalVat = centline.Hetvatval,
                    TotalPriceWithVat = centline.Hettotalval + centline.Hetvatval,
                    MeasurementUnit = "N/A"
                };
                invoiceLines.Add(invoiceLine);
            }

            invoice.InvoiceLines = invoiceLines;
            invoices.Add(invoice);
        }

        //log end of the process
        _logger.LogInformation("Finished building all invoices for user {userProfile}", userProfile);

        return invoices;
    }

    /// <summary>
    ///     Helper function that cuts strings to a maximum length.
    /// </summary>
    /// <param name="str">The string to cut.</param>
    /// <param name="maxLength">The maximum length of the string.</param>
    /// <returns>The cut string.</returns>
    private static string CutString(string str, int maxLength)
    {
        return str.Length > maxLength ? str[..maxLength] : str;
    }
}