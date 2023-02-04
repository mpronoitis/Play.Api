namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/docentries")]
public class PylonDocEntriesController : ApiController
{
    private readonly IPylonDocEntriesService _pylonDocEntriesService;

    public PylonDocEntriesController(IPylonDocEntriesService pylonDocEntriesService)
    {
        _pylonDocEntriesService = pylonDocEntriesService;
    }

    /// <summary>
    ///     Get count of doc entries for a given date range
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>Count of doc entries</returns>
    [HttpGet("count/{startDate}/{endDate}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCount(DateTime startDate, DateTime endDate)
    {
        var count = await _pylonDocEntriesService.GetDocEntriesCountAsync(startDate, endDate);
        return CustomResponse(new { count });
    }
}