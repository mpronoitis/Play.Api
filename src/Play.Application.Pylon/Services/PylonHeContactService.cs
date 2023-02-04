using Microsoft.Extensions.Logging;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Services;

public class PylonHeContactService : IPylonHeContactService
{
    private readonly ILogger _logger;
    private readonly IPylonHeContactRepository _pylonHeContactRepository;

    public PylonHeContactService(IPylonHeContactRepository pylonHeContactRepository,
        ILogger<PylonHeContactService> logger)
    {
        _pylonHeContactRepository = pylonHeContactRepository;
        _logger = logger;
    }

    /// <summary>
    ///     get all hecontacts with pagination
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>The hecontacts</returns>
    public async Task<IEnumerable<PylonContactDTO>> GetPylonContactsAsync(int page, int pageSize)
    {
        try
        {
            var contacts = await _pylonHeContactRepository.GetHecontactsAsync(page, pageSize);
            //create list of PylonContactDTO
            var pylonContacts = contacts.Select(contact => new PylonContactDTO(contact)).ToList();
            return pylonContacts;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting hecontacts - GetPylonContactsAsync");
            //return empty list of PylonContactDTO
            return new List<PylonContactDTO>();
        }
    }

    /// <summary>
    ///     Get contacts by phone number , no pagination , descending order by creation date
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>List of contacts</returns>
    public async Task<IEnumerable<PylonContactDTO>> GetPylonContactsByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            var contacts = await _pylonHeContactRepository.GetContactsByPhoneNumber(phoneNumber);
            return contacts.Select(contact => new PylonContactDTO(contact)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting hecontacts - GetPylonContactsByPhoneNumberAsync");
            //return empty list
            return new List<PylonContactDTO>();
        }
    }

    /// <summary>
    ///     Get contacts by email , no pagination , descending order by creation date
    /// </summary>
    /// <param name="email"></param>
    /// <returns>List of contacts</returns>
    public async Task<IEnumerable<PylonContactDTO>> GetPylonContactsByEmailAsync(string email)
    {
        try
        {
            var contacts = await _pylonHeContactRepository.GetContactsByEmail(email);
            return contacts.Select(contact => new PylonContactDTO(contact)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting hecontacts - GetPylonContactsByEmailAsync");
            //return empty list
            return new List<PylonContactDTO>();
        }
    }

    /// <summary>
    ///     Get contacts by name , no pagination , descending order by creation date
    /// </summary>
    /// <param name="name"></param>
    /// <returns>List of contacts</returns>
    public async Task<IEnumerable<PylonContactDTO>> GetPylonContactsByNameAsync(string name)
    {
        try
        {
            var contacts = await _pylonHeContactRepository.GetContactsByName(name);
            return contacts.Select(contact => new PylonContactDTO(contact)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting hecontacts - GetPylonContactsByNameAsync");
            //return empty list
            return new List<PylonContactDTO>();
        }
    }

    /// <summary>
    ///     Get count of contacts created in a given date range
    /// </summary>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns>Count of contacts</returns>
    public async Task<int> GetPylonContactsCountByDateRangeAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            var count = await _pylonHeContactRepository.GetContactsCountByDateRange(fromDate, toDate);
            return count;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting hecontacts - GetPylonContactsCountByDateRangeAsync");
            //return 0
            return 0;
        }
    }
}