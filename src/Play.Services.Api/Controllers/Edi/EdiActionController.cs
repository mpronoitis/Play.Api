namespace Play.Services.Api.Controllers.Edi;

[Route("edi/actions")]
public class EdiActionController : ApiController
{
    private readonly IEdiActionService _ediActionService;

    public EdiActionController(IEdiActionService ediActionService)
    {
        _ediActionService = ediActionService;
    }

    /// <summary>
    ///     Function to send all un sent EDI files to the EDI partner
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    [HttpGet("send/{customerId:guid}")]
    [Authorize(Roles = "Customer,PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customerId")]
    public async Task<IActionResult> SendEdiFiles(Guid customerId)
    {
        var result = await _ediActionService.SendEdiDocuments(customerId);
        if (result.IsValid) return CustomResponse(new { completed = true });

        //get the first error
        var error = result.Errors.FirstOrDefault();
        if (error != null) AddError(error.ErrorMessage);
        return CustomResponse();
    }


    /// <summary>
    ///     Function to build all un built EDI files for a given customer
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    [HttpGet("build/{customerId:guid}")]
    [Authorize(Roles = "Customer,PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customderId")]
    public async Task<IActionResult> BuildEdiFiles(Guid customerId)
    {
        var result = await _ediActionService.BuildEdiDocuments(customerId);
        if (result.IsValid) return CustomResponse(new { completed = true });

        //get the first error
        var error = result.Errors.FirstOrDefault();
        if (error != null) AddError(error.ErrorMessage);
        return CustomResponse();
    }
}