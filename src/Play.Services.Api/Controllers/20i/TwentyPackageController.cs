namespace Play.Services.Api.Controllers._20i;

[Route("20i/packages")]
public class TwentyPackageController : ApiController
{
    private readonly ILogger<TwentyPackageController> _logger;
    private readonly ITwentyPackageService _twentyPackageService;

    public TwentyPackageController(ITwentyPackageService twentyPackageService, ILogger<TwentyPackageController> logger)
    {
        _twentyPackageService = twentyPackageService;
        _logger = logger;
    }

    [HttpGet("all")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAllPackages()
    {
        try
        {
            var packages = await _twentyPackageService.GetPackages();
            return CustomResponse(packages);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all packages");
            AddError(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetPackageById(string id)
    {
        try
        {
            var package = await _twentyPackageService.GetPackage(id);
            return CustomResponse(package);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting package by id for id {id}", id);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("web-logs/{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetPackagesWebLogs(string id)
    {
        try
        {
            var webLogs = await _twentyPackageService.GetPackagesWebLogs(id);
            return CustomResponse(webLogs);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting package web logs for id {id}", id);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("start-scan/{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetStartMalwareScan(string id)
    {
        try
        {
            var response = await _twentyPackageService.GetStartMalwareScan(id);
            return CustomResponse(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error starting malware scan for id {id}", id);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    [HttpPost("mass-scan")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetStartMassScan([FromBody] List<string> ids)
    {
        try
        {
            var response = await _twentyPackageService.GetStartMassScan(ids);
            return CustomResponse(new { status = response });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error starting mass scan for ids {ids}", ids);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("check-scan/{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCheckMalwareScan(string id)
    {
        try
        {
            var response = await _twentyPackageService.GetCheckMalwareScan(id);
            return CustomResponse(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking malware scan for id {id}", id);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("limits/{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetPackageLimits(string id)
    {
        try
        {
            var package = await _twentyPackageService.GetPackageBundleTypeLimits(id);
            return CustomResponse(package);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting package limits for id {id}", id);
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get total count of packages
    /// </summary>
    [HttpGet("count")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetPackagesCount()
    {
        try
        {
            var count = await _twentyPackageService.GetPackagesCount();
            return CustomResponse(new { count });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting packages count");
            AddError(e.Message);
            return CustomResponse();
        }
    }
}