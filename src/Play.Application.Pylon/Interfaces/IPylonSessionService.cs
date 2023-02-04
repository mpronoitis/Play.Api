using PylonDatabaseHandler.models.pylon;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonSessionService
{
    Task<IEnumerable<Posessions>> GetAllSessionsAsync();
}