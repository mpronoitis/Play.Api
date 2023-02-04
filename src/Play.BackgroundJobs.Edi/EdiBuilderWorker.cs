using Microsoft.Extensions.Logging;
using Play.BackgroundJobs.Edi.Interfaces;
using Play.Domain.Edi.Interfaces;

namespace Play.BackgroundJobs.Edi;

/// <summary>
///     This worker is responsible for processing EDI files that are uploaded to the system and sending them.
/// </summary>
public class EdiBuilderWorker : IEdiBuilderWorker
{
    private readonly IEdiBuilderRepository _ediBuilderRepository;
    private readonly IEdiConnectionRepository _ediConnectionRepository;
    private readonly ILogger _logger;

    public EdiBuilderWorker(IEdiBuilderRepository ediBuilderRepository,
        IEdiConnectionRepository ediConnectionRepository, ILogger<EdiBuilderWorker> logger)
    {
        _ediBuilderRepository = ediBuilderRepository;
        _ediConnectionRepository = ediConnectionRepository;
        _logger = logger;
    }

    /// <summary>
    ///     This is a background service that is intended to run at regular intervals (e.g. every hour) to perform some tasks.
    ///     It starts by logging that the service is running and then gets a list of all the current connections from the
    ///     _ediConnectionRepository repository.
    ///     It extracts all the unique customer ids from these connections and then calls the
    ///     _ediBuilderRepository.BuildUnparsed method for each customer id.
    ///     This method is likely to build an EDI document for the customer based on some data and return it as an unparsed
    ///     string.
    ///     The service also includes a try-catch block to handle any exceptions that might occur during the execution of the
    ///     service.
    ///     If an exception is caught, it logs the error message and the exception object using the _logger object.
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
                var ediBuilder = await _ediBuilderRepository.BuildUnparsed(customerId);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in EdiWorker");
        }
    }
}