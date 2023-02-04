using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonHeContactService
{
    /// <summary>
    ///     get all hecontacts with pagination
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>The hecontacts</returns>
    Task<IEnumerable<PylonContactDTO>> GetPylonContactsAsync(int page, int pageSize);

    /// <summary>
    ///     Get contacts by phone number , no pagination , descending order by creation date
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>List of contacts</returns>
    Task<IEnumerable<PylonContactDTO>> GetPylonContactsByPhoneNumberAsync(string phoneNumber);

    /// <summary>
    ///     Get contacts by email , no pagination , descending order by creation date
    /// </summary>
    /// <param name="email"></param>
    /// <returns>List of contacts</returns>
    Task<IEnumerable<PylonContactDTO>> GetPylonContactsByEmailAsync(string email);

    /// <summary>
    ///     Get contacts by name , no pagination , descending order by creation date
    /// </summary>
    /// <param name="name"></param>
    /// <returns>List of contacts</returns>
    Task<IEnumerable<PylonContactDTO>> GetPylonContactsByNameAsync(string name);

    /// <summary>
    ///     Get count of contacts created in a given date range
    /// </summary>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns>Count of contacts</returns>
    Task<int> GetPylonContactsCountByDateRangeAsync(DateTime fromDate, DateTime toDate);
}