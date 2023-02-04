using Microsoft.Extensions.Logging;
using Play.BackgroundJobs.Edi.Interfaces;
using Play.Domain.Edi.Interfaces;

namespace Play.BackgroundJobs.Edi;

public class EdiSenderWorker : IEdiSenderWorker
{
    private readonly IEdiConnectionRepository _ediConnectionRepository;
    private readonly IEdiSendRepository _ediSendRepository;
    private readonly ILogger _logger;

    public EdiSenderWorker(IEdiSendRepository ediSendRepository, IEdiConnectionRepository ediConnectionRepository,
        ILogger<EdiSenderWorker> logger)
    {
        _ediSendRepository = ediSendRepository;
        _ediConnectionRepository = ediConnectionRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Send all unsent edi doucments to the respective edi connections (FTP)
    /// </summary>
    public async Task DoWork()
    {
        try
        {
            //log that the EdiWorker is running
            _logger.LogInformation("EdiWorker is running at: {time}", DateTimeOffset.Now);
            //get all current connections
            var connections = await _ediConnectionRepository.GetAllAsync(1, 1000);
            //get all unique customer ids
            var customerIds = connections.Select(x => x.Customer_Id).Distinct();
            //call edi builder for each customer id
            foreach (var customerId in customerIds)
            {
                var ediBuilder = await _ediSendRepository.SendUnsentEdiFiles(customerId);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in EdiWorker");
        }
    }
}