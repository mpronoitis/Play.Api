using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonPaymentMethodRepository : IPylonPaymentMethodRepository
{
    private readonly PylonInfraContext _context;

    public PylonPaymentMethodRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database.InitializeContext<PylonInfraContext>(connectionString ??
                                                                 throw new InvalidOperationException(
                                                                     "PylonDatabase connection string not found"))
            .Result;
    }

    /// <summary>
    ///     Get payment method by heid (primary key) GUID
    /// </summary>
    /// <param name="heid">GUID</param>
    /// <returns>PaymentMethod</returns>
    public async Task<Hepaymentmethods?> GetPaymentMethodByHeid(Guid heid)
    {
        return await _context.Hepaymentmethods.FindAsync(heid);
    }
}