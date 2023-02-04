using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonCustomerRepository : IPylonCustomerRepository
{
    private readonly PylonCommercialContext _context;

    public PylonCustomerRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database.InitializeContext<PylonCommercialContext>(connectionString ??
                                                                      throw new InvalidOperationException(
                                                                          "PylonDatabase connection string not found"))
            .Result;
    }

    /// <summary>
    ///     Function to get a single customer based on his HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID of the customer</param>
    /// <returns>The customer</returns>
    /// <exception cref="ArgumentNullException">Thrown when the HEID is null</exception>
    /// <exception cref="ArgumentException">Thrown when the HEID is empty</exception>
    public async Task<Hecustomers?> GetCustomerByHeidAsync(Guid heid)
    {
        if (heid == Guid.Empty)
            throw new ArgumentException("The HEID cannot be empty", nameof(heid));

        return await _context.Hecustomers.FirstOrDefaultAsync(c => c.Heid == heid);
    }
}