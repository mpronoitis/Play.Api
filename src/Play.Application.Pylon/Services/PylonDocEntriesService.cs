using Microsoft.Extensions.Logging;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;

namespace Play.Application.Pylon.Services;

public class PylonDocEntriesService : IPylonDocEntriesService
{
    private readonly ILogger<PylonDocEntriesService> _logger;
    private readonly IPylonDocentriesRepository _pylonDocentriesRepository;

    public PylonDocEntriesService(IPylonDocentriesRepository pylonDocentriesRepository,
        ILogger<PylonDocEntriesService> logger)
    {
        _pylonDocentriesRepository = pylonDocentriesRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Get count of docentries for a given date range
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>Count of docentries</returns>
    public async Task<int> GetDocEntriesCountAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            return await _pylonDocentriesRepository.GetDocentriesCountAsync(startDate, endDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting docentries count");
            return 0;
        }
    }
}