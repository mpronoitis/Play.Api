namespace Play.Services.Api.Controllers.Epp;

[Route("epp/registrant")]

public class EppAccountRegistrantController: ApiController
{
    private readonly IEppAccountRegistrantService _eppAccountRegistrantService;
    private readonly ILogger<EppAccountRegistrantController> _logger;
    
    
    public EppAccountRegistrantController(IEppAccountRegistrantService eppAccountRegistrantService, ILogger<EppAccountRegistrantController> logger)
    {
        _eppAccountRegistrantService = eppAccountRegistrantService;
        _logger = logger;
    } 
    
    [HttpGet("info")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> GetAccountRegistrantInfo()
    {
        try
        {
            var result = await _eppAccountRegistrantService.GetAccountRegistrantInfo();
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on GetAccountRegistrantInfo");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

}