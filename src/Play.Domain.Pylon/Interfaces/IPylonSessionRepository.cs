using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonSessionRepository
{
    Task<IEnumerable<Posessions>> GetAllSessionsAsync();
}