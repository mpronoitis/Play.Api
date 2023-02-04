namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/item")]
public class PylonItemController : ApiController
{
    private readonly IPylonItemService _pylonItemService;

    public PylonItemController(IPylonItemService pylonItemService)
    {
        _pylonItemService = pylonItemService;
    }

    /// <summary>
    ///     Get all pylon items with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    [HttpGet("all/{page}/{pageSize}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        try
        {
            var items = await _pylonItemService.GetPylonItemsAsync(page, pageSize);
            return CustomResponse(items);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get Items by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetByName(string name)
    {
        try
        {
            var items = await _pylonItemService.GetPylonItemsByNameAsync(name);
            return CustomResponse(items);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get total count of Items
    /// </summary>
    /// <returns></returns>
    [HttpGet("count")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCount()
    {
        try
        {
            var count = await _pylonItemService.GetPylonItemsCountAsync();
            return CustomResponse(new { count });
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get total count of items created in a given date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [HttpGet("count/{startDate}/{endDate}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCount(DateTime startDate, DateTime endDate)
    {
        try
        {
            var res = await _pylonItemService.GetPylonItemsCountByDateRangeAsync(startDate, endDate);
            return CustomResponse(new { count = res });
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}