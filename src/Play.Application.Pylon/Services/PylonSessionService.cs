using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler.models.pylon;

namespace Play.Application.Pylon.Services;

public class PylonSessionService : IPylonSessionService
{
    private readonly IPylonSessionRepository _pylonSessionRepository;

    public PylonSessionService(IPylonSessionRepository pylonSessionRepository
    )
    {
        _pylonSessionRepository = pylonSessionRepository;
    }

    public async Task<IEnumerable<Posessions>> GetAllSessionsAsync()
    {
        return await _pylonSessionRepository.GetAllSessionsAsync();
    }
}