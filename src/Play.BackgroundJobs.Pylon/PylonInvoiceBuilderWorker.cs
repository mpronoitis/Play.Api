using Play.Application.Pylon.Interfaces;
using Play.BackgroundJobs.Pylon.Interfaces;
using Play.Domain.Core.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.BackgroundJobs.Pylon;

public class PylonInvoiceBuilderWorker : IPylonInvoiceBuilderWorker
{
    private readonly IPylonInvoiceBuilderService _pylonInvoiceBuilderService;
    private readonly IPylonInvoiceService _pylonInvoiceService;

    private readonly IUserProfileRepository _userProfileRepository;

    public PylonInvoiceBuilderWorker(IUserProfileRepository userProfileRepository,
        IPylonInvoiceBuilderService pylonInvoiceBuilderService, IPylonInvoiceService pylonInvoiceService)
    {
        _userProfileRepository = userProfileRepository;
        _pylonInvoiceBuilderService = pylonInvoiceBuilderService;
        _pylonInvoiceService = pylonInvoiceService;
    }

    public async Task DoWork()
    {
        //get all user profiles
        var userProfiles = await _userProfileRepository.GetAllAsync();
        //delete all profiles that tin is equal to null or empty or 0
        userProfiles = userProfiles.Where(x => !string.IsNullOrEmpty(x.TIN) && x.TIN != "0").ToList();
        //for each user profile get the invoices
        var invoices = new List<PylonInvoice>();

        //delete all invoices from the database based on customer tin
        foreach (var userProfile in userProfiles)
        {
            var delResult = await _pylonInvoiceService.DeleteInvoicesAsync(userProfile.TIN);
        }

        //build the invoices
        foreach (var userProfile in userProfiles)
        {
            var pylonInvoices = await _pylonInvoiceBuilderService.BuildAll(userProfile.User_Id);
            invoices.AddRange(pylonInvoices);
        }

        foreach (var invoice in invoices) await _pylonInvoiceService.AddInvoiceAsync(invoice);
    }
}