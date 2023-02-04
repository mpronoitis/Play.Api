using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonCentlineRepository
{
    /// <summary>
    ///     Function to get a Centline by its HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID GUID</param>
    /// <returns>The Centline</returns>
    /// <exception cref="ArgumentNullException">Thrown when the HEID is null</exception>
    /// <exception cref="ArgumentException">Thrown when the HEID is empty</exception>
    Task<Hecentlines?> GetCentlineByHeidAsync(Guid heid);

    /// <summary>
    ///     Function to get all centlines based on a Hedentid
    /// </summary>
    /// <param name="hedentid">The Hedentid</param>
    /// <returns>A list of centlines</returns>
    /// <exception cref="ArgumentNullException">Thrown when the Hedentid is null</exception>
    /// <exception cref="ArgumentException">Thrown when the Hedentid is empty</exception>
    Task<List<Hecentlines>> GetCentlinesByHedentidAsync(Guid hedentid);
}