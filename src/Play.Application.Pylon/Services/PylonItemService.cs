using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Services;

public class PylonItemService : IPylonItemService
{
    private readonly IPylonHeItemRepository _pylonHeItemRepository;
    private readonly IPylonItemRepository _pylonItemRepository;

    public PylonItemService(IPylonItemRepository pylonItemRepository, IPylonHeItemRepository pylonHeItemRepository)
    {
        _pylonItemRepository = pylonItemRepository;
        _pylonHeItemRepository = pylonHeItemRepository;
    }

    /// <summary>
    ///     Get all pylon items with paging
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonItem>> GetPylonItemsAsync(int page = 1, int pageSize = 10)
    {
        return await _pylonItemRepository.GetAll(page, pageSize);
    }

    /// <summary>
    ///     Get Items by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonItem>> GetPylonItemsByNameAsync(string name)
    {
        return await _pylonItemRepository.GetByName(name);
    }

    /// <summary>
    ///     Get total count of Items
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetPylonItemsCountAsync()
    {
        return await _pylonItemRepository.GetCount();
    }

    /// <summary>
    ///     Get count of items created in a given date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<int> GetPylonItemsCountByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _pylonHeItemRepository.GetHeitemsCountByPeriod(startDate, endDate);
    }
}