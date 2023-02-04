using FluentValidation.Results;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonInvoiceService
{
    /// <summary>
    ///     Adds a new invoice to the database.
    /// </summary>
    /// <param name="invoice">The invoice to add.</param>
    /// <returns>Vaidation result.</returns>
    Task<ValidationResult> AddInvoiceAsync(PylonInvoice invoice);

    /// <summary>
    ///     Deletes all invoices with a given TIN from the database.
    /// </summary>
    /// <param name="tin">The TIN of the customer.</param>
    /// <returns>Validation result.</returns>
    Task<ValidationResult> DeleteInvoicesAsync(string tin);

    /// <summary>
    ///     Function to get all pylon invoices for a given customer id with paging
    /// </summary>
    /// <param name="customerId">The customer id</param>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>A list of pylon invoices</returns>
    Task<IEnumerable<PylonInvoice>> GetPylonInvoicesAsync(Guid customerId, int page, int pageSize);
}