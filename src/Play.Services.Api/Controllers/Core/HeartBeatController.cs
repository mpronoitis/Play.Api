namespace Play.Services.Api.Controllers.Core;

/// <summary>
///     Controller for docker health checks
///     This controller is used by the docker health check to determine if the service is healthy
///     The health check is configured in the docker-compose.yml file
/// </summary>
[Route("heartbeat")]
[RateLimit(PeriodInSec = 10, Limit = 10)]
public class HeartBeatController : ApiController
{
    [AllowAnonymous]
    [HttpGet("check")]
    public IActionResult GetHeartBeat()
    {
        return CustomResponse(new { alive = true });
    }
}