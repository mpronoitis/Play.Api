namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/commercialentries")]
public class PylonCommercialEntriesController : ApiController
{
    private readonly IPylonCommercialEntriesService _pylonCommercialEntriesService;

    public PylonCommercialEntriesController(IPylonCommercialEntriesService pylonCommercialEntriesService)
    {
        _pylonCommercialEntriesService = pylonCommercialEntriesService;
    }

    /// <summary>
    ///     Get total income for a given date range
    /// </summary>
    /// <param name="from">Start date</param>
    /// <param name="to">End date</param>
    /// <returns></returns>
    [HttpGet("income/{from}/{to}")]
    public async Task<IActionResult> GetIncome(DateTime from, DateTime to)
    {
        var result = await _pylonCommercialEntriesService.GetTotalIncome(from, to);
        return CustomResponse(new { result });
    }
}