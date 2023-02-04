using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonCustomerRepository
{
    /// <summary>
    ///     Function to get a single customer based on his HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID of the customer</param>
    /// <returns>The customer</returns>
    /// <exception cref="ArgumentNullException">Thrown when the HEID is null</exception>
    /// <exception cref="ArgumentException">Thrown when the HEID is empty</exception>
    Task<Hecustomers?> GetCustomerByHeidAsync(Guid heid);
}