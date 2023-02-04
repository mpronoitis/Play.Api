namespace Play.Application.Pylon.Interfaces;

public interface IPylonContactMigrationService
{
    /// <summary>
    ///     Function that migrates the contacts from the pylon db to the PlayPylon db
    /// </summary>
    /// <returns></returns>
    Task MigrateHeContacts();
}