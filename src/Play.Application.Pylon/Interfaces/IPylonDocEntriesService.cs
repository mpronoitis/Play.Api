namespace Play.Application.Pylon.Interfaces;

public interface IPylonDocEntriesService
{
    /// <summary>
    ///     Get count of docentries for a given date range
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>Count of docentries</returns>
    Task<int> GetDocEntriesCountAsync(DateTime startDate, DateTime endDate);
}