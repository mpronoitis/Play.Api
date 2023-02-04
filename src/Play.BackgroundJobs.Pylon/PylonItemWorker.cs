using Play.Application.Pylon.Interfaces;

namespace Play.BackgroundJobs.Pylon;

public class PylonItemWorker
{
    private readonly IPylonItemMigrationService _pylonItemMigrationService;

    public PylonItemWorker(IPylonItemMigrationService pylonItemMigrationService)
    {
        _pylonItemMigrationService = pylonItemMigrationService;
    }

    public async Task DoWork()
    {
        await _pylonItemMigrationService.MigrateHeItems();
    }
}