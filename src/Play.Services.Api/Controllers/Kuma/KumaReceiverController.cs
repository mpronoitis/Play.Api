namespace Play.Services.Api.Controllers.Kuma;

[Route("kuma/receive")]
public class KumaReceiverController : ApiController
{
    private readonly IKumaNotificationService _kumaNotificationService;
    private readonly ILogger<KumaReceiverController> _logger;

    public KumaReceiverController(IKumaNotificationService kumaNotificationService,
        ILogger<KumaReceiverController> logger)
    {
        _kumaNotificationService = kumaNotificationService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] KumaNotificationViewModel request)
    {
        try
        {
            await _kumaNotificationService.ReceiveNotificationAsync(request);
            return CustomResponse(new { message = "Notification received successfully" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error receiving notification");
            AddError(e.Message);
            return CustomResponse();
        }
    }
}