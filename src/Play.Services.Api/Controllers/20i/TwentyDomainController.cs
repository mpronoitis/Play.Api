namespace Play.Services.Api.Controllers._20i;

[Route("20i/domains")]
public class TwentyDomainController : ApiController
{
    private readonly ILogger<TwentyDomainController> _logger;
    private readonly ITwentyDomainService _twentyDomainService;

    public TwentyDomainController(ITwentyDomainService twentyDomainService, ILogger<TwentyDomainController> logger)
    {
        _twentyDomainService = twentyDomainService;
        _logger = logger;
    }

    [HttpGet("all")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _twentyDomainService.GetDomains();
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all domains");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("search/{domain}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Search(string domain)
    {
        try
        {
            var result = await _twentyDomainService.SearchDomain(domain);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching domain {domain}", domain);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("periods")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetPeriods()
    {
        try
        {
            var result = await _twentyDomainService.GetDomainPeriods();
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting domain periods");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get total count of domains
    /// </summary>
    [HttpGet("count")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCount()
    {
        try
        {
            var result = await _twentyDomainService.GetDomainCount();
            return CustomResponse(new { count = result });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting domain count");
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}