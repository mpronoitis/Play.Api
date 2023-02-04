using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonMeasurementUnitRepository : IPylonMeasurementUnitRepository
{
    private readonly PylonCommercialContext _context;

    public PylonMeasurementUnitRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonCommercialContext>(connectionString ??
                                                       throw new InvalidOperationException(
                                                           "Connection string not found")).Result;
    }

    public async Task<Hemeasurementunits?> GetHemeasurementunits(Guid heid)
    {
        return await _context.Hemeasurementunits.FindAsync(heid);
    }
}