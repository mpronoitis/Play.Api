using Microsoft.Extensions.Logging;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;

namespace Play.Application.Pylon.Services;

public class PylonCommercialEntriesService : IPylonCommercialEntriesService
{
    private readonly ILogger<PylonCommercialEntriesService> _logger;
    private readonly IPylonCommercialEntriesRepository _pylonCommercialEntriesRepository;

    public PylonCommercialEntriesService(IPylonCommercialEntriesRepository pylonCommercialEntriesRepository,
        ILogger<PylonCommercialEntriesService> logger)
    {
        _pylonCommercialEntriesRepository = pylonCommercialEntriesRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Get total income for a given time range
    /// </summary>
    /// <param name="from">Start date</param>
    /// <param name="to">End date</param>
    /// <returns>Total income</returns>
    public async Task<decimal> GetTotalIncome(DateTime from, DateTime to)
    {
        try
        {
            return await _pylonCommercialEntriesRepository.GetTotalIncomeAsync(from, to);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting total income");
            return 0;
        }
    }
}