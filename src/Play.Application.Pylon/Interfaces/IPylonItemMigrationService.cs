namespace Play.Application.Pylon.Interfaces;

public interface IPylonItemMigrationService
{
    /// <summary>
    ///     Function that migrates the heitems from pylon to Play.Pylon database
    /// </summary>
    /// <returns></returns>
    Task MigrateHeItems();
}