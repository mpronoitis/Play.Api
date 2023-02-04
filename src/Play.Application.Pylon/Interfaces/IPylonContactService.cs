using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonContactService
{
    /// <summary>
    ///     Get all PylonContacts with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<PylonContact>> GetPylonContactsAsync(int page, int pageSize);

    /// <summary>
    ///     Search for PylonContacts
    /// </summary>
    /// <param name="query">Query to search</param>
    /// <param name="name">If we want to search by name</param>
    /// <param name="phone">If we want to search by phone</param>
    /// <param name="email">If we want to search by email</param>
    /// <param name="address">If we want to search by address</param>
    /// <returns></returns>
    Task<IEnumerable<PylonContact>> SearchPylonContactsAsync(string query, bool name, bool phone, bool email,
        bool address);
}