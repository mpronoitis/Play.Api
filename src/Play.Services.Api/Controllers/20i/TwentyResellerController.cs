namespace Play.Services.Api.Controllers._20i;

[Route("20i/reseller")]
public class TwentyResellerController : ApiController
{
    private readonly ILogger<TwentyResellerController> _logger;
    private readonly ITwentyResellerService _twentyResellerService;

    public TwentyResellerController(ITwentyResellerService twentyResellerService,
        ILogger<TwentyResellerController> logger)
    {
        _twentyResellerService = twentyResellerService;
        _logger = logger;
    }

    /// <summary>
    ///     Renew a domain name.
    ///     This will charge the appropriate registration fee to your 20i Balance.
    ///     If you don't have enough left, this will fail.
    /// </summary>
    /// <param name="domainName">The domain name to renew.</param>
    /// <param name="period">The number of years to renew for.</param>
    /// <returns>json string</returns>
    [HttpGet("renew-domain/{domainName}/{period}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> RenewDomain(string domainName, int period)
    {
        try
        {
            var result = await _twentyResellerService.RenewDomain(domainName, period);
            return CustomResponse(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error renewing domain {domainName}", domainName);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get package type information.
    /// </summary>
    /// <returns>json string</returns>
    [HttpGet("get-package-types")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetPackageTypes()
    {
        try
        {
            var result = await _twentyResellerService.GetPackageTypes();
            return CustomResponse(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting package types");
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Returns your Stack user config.
    /// </summary>
    /// <returns>json string</returns>
    [HttpGet("get-stackusers")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetStackUsers()
    {
        try
        {
            var result = await _twentyResellerService.GetStackUserConfig();
            return CustomResponse(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting stack users");
            AddError(e.Message);
            return CustomResponse();
        }
    }
}