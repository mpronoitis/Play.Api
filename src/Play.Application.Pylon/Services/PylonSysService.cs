using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;

namespace Play.Application.Pylon.Services;

public class PylonSysService : IPylonSysService
{
    private readonly IPylonSysRepository _pylonSysRepository;

    public PylonSysService(IPylonSysRepository pylonSysRepository
    )
    {
        _pylonSysRepository = pylonSysRepository;
    }

    public async Task<string> GetByKey(string pokey)
    {
        return await _pylonSysRepository.GetByKey(pokey);
    }
}