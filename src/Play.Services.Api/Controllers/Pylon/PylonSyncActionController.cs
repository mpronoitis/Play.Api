namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/sync")]
public class PylonSyncActionController : ApiController
{
    private readonly ILogger<PylonSyncActionController> _logger;
    private readonly IPylonContactWorker _pylonContactWorker;
    private readonly IPylonInvoiceBuilderWorker _pylonInvoiceBuilderWorker;

    public PylonSyncActionController(IPylonInvoiceBuilderWorker pylonInvoiceBuilderWorker,
        IPylonContactWorker pylonContactWorker, ILogger<PylonSyncActionController> logger)
    {
        _pylonInvoiceBuilderWorker = pylonInvoiceBuilderWorker;
        _pylonContactWorker = pylonContactWorker;
        _logger = logger;
    }

    /// <summary>
    ///     Initiate a sync of the Pylon Invoice Builder
    /// </summary>
    /// <returns></returns>
    [HttpPost("invoicebuilder")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> SyncInvoiceBuilder()
    {
        try
        {
            await _pylonInvoiceBuilderWorker.DoWork();
            return CustomResponse(new { message = "Sync of Pylon Invoice Builder Completed" });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error syncing Pylon Invoice Builder");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Initiate a sync of the Pylon Contact
    /// </summary>
    /// <returns></returns>
    [HttpPost("contacts")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> SyncContacts()
    {
        try
        {
            await _pylonContactWorker.DoWork();
            return CustomResponse(new { message = "Sync of Pylon Contacts Completed" });
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}