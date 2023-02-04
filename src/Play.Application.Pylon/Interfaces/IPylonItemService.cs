using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonItemService
{
    /// <summary>
    ///     Get all pylon items with paging
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<PylonItem>> GetPylonItemsAsync(int page = 1, int pageSize = 10);

    /// <summary>
    ///     Get total count of Items
    /// </summary>
    /// <returns></returns>
    Task<int> GetPylonItemsCountAsync();

    /// <summary>
    ///     Get Items by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<IEnumerable<PylonItem>> GetPylonItemsByNameAsync(string name);

    /// <summary>
    ///     Get count of items created in a given date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<int> GetPylonItemsCountByDateRangeAsync(DateTime startDate, DateTime endDate);
}