namespace Play.Services.Api.Controllers.Epp;

[Route("epp/domain")]
public class EppDomainController : ApiController
{
    private readonly IEppDomainService _eppDomainService;
    private readonly ILogger<EppDomainController> _logger;

    public EppDomainController(IEppDomainService eppDomainService, ILogger<EppDomainController> logger)
    {
        _eppDomainService = eppDomainService;
        _logger = logger;
    }

    [HttpGet("check/{domainName}")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> CheckDomain(string domainName)
    {
        try
        {
            var result = await _eppDomainService.CheckDomainAvailability(domainName);
            return CustomResponse(new { available = result });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking domain availability for {domainName}", domainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("info/{domainName}")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> GetDomainInfo(string domainName)
    {
        try
        {
            var result = await _eppDomainService.GetDomainInfo(domainName);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting domain info for {domainName}", domainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Register a new domain
    /// </summary>
    /// <param name="registerDomainViewModel">Domain information</param>
    /// <returns>Domain registration result</returns>
    [HttpPost("register")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> RegisterDomain([FromBody] RegisterEppDomainViewModel registerDomainViewModel)
    {
        try
        {
            var result = await _eppDomainService.RegisterDomain(registerDomainViewModel);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering domain {domainName}", registerDomainViewModel.DomainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Transfer a domain
    /// </summary>
    /// <param name="transferDomainViewModel">Domain information</param>
    /// <returns>Domain transfer result</returns>
    [HttpPost("transfer")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> TransferDomain([FromBody] TransferEppDomainViewModel transferDomainViewModel)
    {
        try
        {
            var result = await _eppDomainService.TransferDomain(transferDomainViewModel);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error transferring domain {domainName}", transferDomainViewModel.DomainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Renew a domain
    /// </summary>
    /// <param name="renewDomainViewModel">Domain information</param>
    /// <returns>Domain renewal result</returns>
    [HttpPost("renew")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> RenewDomain([FromBody] RenewEppDomainViewModel renewDomainViewModel)
    {
        try
        {
            var result = await _eppDomainService.RenewDomain(renewDomainViewModel);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error renewing domain {domainName}", renewDomainViewModel.DomainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Add a nameserver to a domain
    /// </summary>
    /// <param name="addNameserverViewModel">Domain information</param>
    /// <returns>Domain nameserver addition result</returns>
    [HttpPost("nameserver/add")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> AddNameserver([FromBody] RegisterEppNameserverViewModel addNameserverViewModel)
    {
        try
        {
            var result = await _eppDomainService.AddNameserver(addNameserverViewModel);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding nameserver {nameserver} to domain {domainName}",
                addNameserverViewModel.Nameserver, addNameserverViewModel.DomainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Remove all nameservers from a domain
    /// </summary>
    /// <param name="domainName">Domain name</param>
    /// <returns>Domain nameserver removal result</returns>
    [HttpPost("nameserver/remove/{domainName}")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> RemoveNameservers(string domainName)
    {
        try
        {
            var result = await _eppDomainService.RemoveAllNameservers(domainName);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing all nameservers from domain {domainName}", domainName);
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Add a list of nameservers to a domain
    /// </summary>
    /// <param name="addNameserversViewModel">Domain information</param>
    /// <returns>Domain nameserver addition result</returns>
    [HttpPost("nameservers/add")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> AddNameservers([FromBody] RegisterEppNameserversViewModel addNameserversViewModel)
    {
        try
        {
            var result = await _eppDomainService.AddNameservers(addNameserversViewModel);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding nameservers to domain {domainName}", addNameserversViewModel.Domain);
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}