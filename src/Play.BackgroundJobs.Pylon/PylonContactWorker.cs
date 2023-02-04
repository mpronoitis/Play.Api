using Play.Application.Pylon.Interfaces;
using Play.BackgroundJobs.Pylon.Interfaces;

namespace Play.BackgroundJobs.Pylon;

public class PylonContactWorker : IPylonContactWorker
{
    private readonly IPylonContactMigrationService _pylonContactMigrationService;

    public PylonContactWorker(IPylonContactMigrationService pylonContactMigrationService)
    {
        _pylonContactMigrationService = pylonContactMigrationService;
    }

    public async Task DoWork()
    {
        await _pylonContactMigrationService.MigrateHeContacts();
    }
}