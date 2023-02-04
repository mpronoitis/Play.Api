using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonDocseriesRepository
{
    /// <summary>
    ///     Get a single document series by its HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID GUID</param>
    /// <returns>The document series</returns>
    Task<Hedocseries?> GetDocseriesByHeidAsync(Guid heid);

    /// <summary>
    ///     Get a single document series by its hename
    /// </summary>
    /// <param name="hename">The hename</param>
    /// <returns>The document series</returns>
    Task<Hedocseries?> GetDocseriesByHenameAsync(string hename);
}