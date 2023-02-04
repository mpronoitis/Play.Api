using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Services;

public class PylonContactService : IPylonContactService
{
    private readonly IPylonTempContactRepository _pylonTempContactRepository;

    public PylonContactService(IPylonTempContactRepository pylonTempContactRepository)
    {
        _pylonTempContactRepository = pylonTempContactRepository;
    }

    /// <summary>
    ///     Get all PylonContacts with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonContact>> GetPylonContactsAsync(int page, int pageSize)
    {
        var pylonContacts = await _pylonTempContactRepository.GetAll(page, pageSize);
        return pylonContacts;
    }

    /// <summary>
    ///     Search for PylonContacts
    /// </summary>
    /// <param name="query">Query to search</param>
    /// <param name="name">If we want to search by name</param>
    /// <param name="phone">If we want to search by phone</param>
    /// <param name="email">If we want to search by email</param>
    /// <param name="address">If we want to search by address</param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonContact>> SearchPylonContactsAsync(string query, bool name, bool phone,
        bool email, bool address)
    {
        var pylonContacts = await _pylonTempContactRepository.Search(query, name, phone, email, address);
        return pylonContacts;
    }
}