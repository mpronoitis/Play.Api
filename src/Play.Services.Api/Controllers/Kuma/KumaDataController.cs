namespace Play.Services.Api.Controllers.Kuma;

[Route("kuma")]
public class KumaDataController : ApiController
{
    private readonly IKumaNotificationService _kumaNotificationService;
    private readonly ILogger<KumaDataController> _logger;

    public KumaDataController(IKumaNotificationService kumaNotificationService, ILogger<KumaDataController> logger)
    {
        _kumaNotificationService = kumaNotificationService;
        _logger = logger;
    }

    [HttpGet("all/{page:int=1}/{pageSize:int=10}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        try
        {
            var notifications = await _kumaNotificationService.GetAllAsync(page, pageSize);
            return CustomResponse(notifications);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all notifications");
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get latest entry for a given url , order by receivedAt desc
    ///     If InvalidOperationException("No notification found for given url") is thrown, it means that there is no entry for
    ///     the given url, return 404
    /// </summary>
    /// <param name="url">The url to search for</param>
    /// <returns></returns>
    [HttpGet("latest/{url}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetLatest(string url)
    {
        try
        {
            var notification = await _kumaNotificationService.GetLatestByUrlAsync(url);
            return CustomResponse(notification);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "Error getting latest notification for url {url}", url);
            AddError(e.Message);
            return NotFound();
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Return incidents for a given url and a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="url"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    [HttpGet("incidents/{url}/{from}/{to}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetIncidents(string url, DateTime from, DateTime to)
    {
        try
        {
            var notifications = await _kumaNotificationService.GetIncidentsByUrlAsync(url, from, to);
            return CustomResponse(notifications);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting incidents for url {url}", url);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Return incidents for a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    [HttpGet("incidents/all/{from}/{to}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetIncidents(DateTime from, DateTime to)
    {
        try
        {
            var notifications = await _kumaNotificationService.GetIncidentsByTimeRangeAsync(from, to);
            return CustomResponse(notifications);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting incidents for time range {from} - {to}", from, to);
            AddError(e.Message);
            return CustomResponse();
        }
    }
}