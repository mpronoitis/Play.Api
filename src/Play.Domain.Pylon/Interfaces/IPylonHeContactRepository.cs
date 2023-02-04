using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonHeContactRepository
{
    /// <summary>
    ///     Function to get a single hecontact by its heid (primary key)
    /// </summary>
    /// <param name="heid">The heid of the hecontact</param>
    /// <returns>The hecontact</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heid is null</exception>
    Task<Hecontacts?> GetHecontactByHeidAsync(Guid heid);

    /// <summary>
    ///     Function to get all hecontacts with pagination
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>The hecontacts</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the page or page size is less than 1</exception>
    /// <exception cref="ArgumentException">Thrown when the page size is greater than 100</exception>
    Task<IEnumerable<Hecontacts>> GetHecontactsAsync(int page, int pageSize);

    /// <summary>
    ///     Function to get a single hecontact by its TIN (Tax Identification Number)
    /// </summary>
    /// <param name="tin">The TIN of the hecontact</param>
    /// <returns>The hecontact</returns>
    /// <exception cref="ArgumentNullException">Thrown when the TIN is null</exception>
    /// <exception cref="ArgumentException">Thrown when the TIN is empty</exception>
    Task<Hecontacts?> GetHecontactByTinAsync(string tin);

    /// <summary>
    ///     Get all contacts , no pagination , no ordering
    /// </summary>
    /// <returns></returns>
    Task<List<Hecontacts>> GetAllContacts();

    /// <summary>
    ///     Get contacts by name , no pagination , descending order by creation date
    /// </summary>
    /// <param name="name"></param>
    /// <returns>List of contacts</returns>
    Task<List<Hecontacts>> GetContactsByName(string name);

    /// <summary>
    ///     Get contacts by phone number , no pagination , descending order by creation date
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>List of contacts</returns>
    Task<List<Hecontacts>> GetContactsByPhoneNumber(string phoneNumber);

    /// <summary>
    ///     Get contacts by email , no pagination , descending order by creation date
    /// </summary>
    /// <param name="email"></param>
    /// <returns>List of contacts</returns>
    Task<List<Hecontacts>> GetContactsByEmail(string email);

    /// <summary>
    ///     Get count of contacts created in a given date range
    /// </summary>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    Task<int> GetContactsCountByDateRange(DateTime fromDate, DateTime toDate);
}