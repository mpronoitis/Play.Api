using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonDocentriesRepository
{
    /// <summary>
    ///     Get a docentry based on its heid (GUID)
    /// </summary>
    /// <param name="heid">The heid of the docentry</param>
    /// <returns>The docentry</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heid is null</exception>
    /// <exception cref="ArgumentException">Thrown when the heid is empty</exception>
    Task<Hedocentries?> GetDocentryByHeidAsync(Guid heid);

    /// <summary>
    ///     Get docentries count based on a given time range
    /// </summary>
    /// <param name="from">The start of the time range</param>
    /// <param name="to">The end of the time range</param>
    /// <returns>The number of docentries</returns>
    Task<int> GetDocentriesCountAsync(DateTime from, DateTime to);
}