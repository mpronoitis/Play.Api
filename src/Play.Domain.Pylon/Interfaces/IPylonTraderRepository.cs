using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonTraderRepository
{
    /// <summary>
    ///     Function to get a trader by his heid (primary key) GUID
    /// </summary>
    /// <param name="heid">The GUID of the trader</param>
    /// <returns>The trader</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heid is null</exception>
    /// <exception cref="ArgumentException">Thrown when the heid is empty</exception>
    Task<Hetraders?> GetTraderByHeidAsync(Guid heid);

    /// <summary>
    ///     Function to get a trader by HECntcID (foreign key) GUID
    /// </summary>
    /// <param name="heCntcId">The Contact Id of the trader</param>
    /// <returns>The trader</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heCntcId is null</exception>
    /// <exception cref="ArgumentException">Thrown when the heCntcId is empty</exception>
    Task<Hetraders?> GetTraderByHeCntcIdAsync(Guid heCntcId);
}