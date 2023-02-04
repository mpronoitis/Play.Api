namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/invoices")]
public class PylonInvoiceController : ApiController
{
    private readonly IPylonInvoiceService _pylonInvoiceService;

    public PylonInvoiceController(IPylonInvoiceService pylonInvoiceService)
    {
        _pylonInvoiceService = pylonInvoiceService;
    }

    [Authorize(Roles = "PlayAdmin,Customer")]
    [HttpGet("{id}/{page}/{pageSize}")]
    public async Task<IActionResult> GetInvoiceById(Guid id, int page, int pageSize)
    {
        try
        {
            var invoice = await _pylonInvoiceService.GetPylonInvoicesAsync(id, page, pageSize);
            return CustomResponse(invoice);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}