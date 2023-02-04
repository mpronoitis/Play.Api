using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Core.Interfaces;
using Play.Domain.Pylon.Commands;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Services;

/// <summary>
///     Service for generating pylon invoices for a given customer.
/// </summary>
public class PylonInvoiceService : IPylonInvoiceService
{
    private readonly IMediatorHandler _mediatorHandler;

    private readonly IPylonInvoiceRepository _pylonInvoiceRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public PylonInvoiceService(IPylonInvoiceRepository pylonInvoiceRepository, IMediatorHandler mediatorHandler,
        IUserProfileRepository userProfileRepository)
    {
        _pylonInvoiceRepository = pylonInvoiceRepository;
        _mediatorHandler = mediatorHandler;
        _userProfileRepository = userProfileRepository;
    }

    /// <summary>
    ///     Function to get all pylon invoices for a given customer id with paging
    /// </summary>
    /// <param name="customerId">The customer id</param>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>A list of pylon invoices</returns>
    public async Task<IEnumerable<PylonInvoice>> GetPylonInvoicesAsync(Guid customerId, int page, int pageSize)
    {
        //get user profile by customer id
        var userProfile = await _userProfileRepository.GetByUserId(customerId);
        //if no user profile found, return empty list
        if (userProfile == null)
            return new List<PylonInvoice>();

        //get pylon invoices by customer TIN
        var pylonInvoices = await _pylonInvoiceRepository.GetByCustomerTin(userProfile.TIN, page, pageSize);

        //return pylon invoices
        return pylonInvoices;
    }


    /// <summary>
    ///     Adds a new invoice to the database.
    /// </summary>
    /// <param name="invoice">The invoice to add.</param>
    /// <returns>Vaidation result.</returns>
    public async Task<ValidationResult> AddInvoiceAsync(PylonInvoice invoice)
    {
        var command = new RegisterPylonInvoiceCommand(invoice);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Deletes all invoices with a given TIN from the database.
    /// </summary>
    /// <param name="tin">The TIN of the customer.</param>
    /// <returns>Validation result.</returns>
    public async Task<ValidationResult> DeleteInvoicesAsync(string tin)
    {
        //get all invoices with the given TIN
        var invoices = await _pylonInvoiceRepository.GetByCustomerTin(tin);

        //convert to list
        var invoicesList = invoices.ToList();
        //create list of validation results
        var validationResults = new List<ValidationResult>();

        //delete all invoices
        foreach (var command in invoicesList.Select(invoice => new RemovePylonInvoiceCommand(invoice.Id)))
            validationResults.Add(await _mediatorHandler.SendCommand(command));

        //if there are any validation errors concatenate them into one result
        if (validationResults.Any(result => !result.IsValid))
        {
            var errors = new List<ValidationFailure>();
            foreach (var validationResult in validationResults) errors.AddRange(validationResult.Errors);
            return new ValidationResult(errors);
        }

        //create new success result
        return new ValidationResult();
    }
}