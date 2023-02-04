using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonHeContactRepository : IPylonHeContactRepository
{
    private readonly PylonCrmContext _context;

    public PylonHeContactRepository(IPylonDatabase database, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database.InitializeContext<PylonCrmContext>(connectionString ??
                                                               throw new InvalidOperationException(
                                                                   "Pylon Connection string is missing")).Result;
    }

    /// <summary>
    ///     Function to get a single hecontact by its heid (primary key)
    /// </summary>
    /// <param name="heid">The heid of the hecontact</param>
    /// <returns>The hecontact</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heid is null</exception>
    public async Task<Hecontacts?> GetHecontactByHeidAsync(Guid heid)
    {
        if (heid == Guid.Empty) throw new ArgumentNullException(nameof(heid));

        return await _context.Hecontacts.FindAsync(heid) ?? null;
    }

    /// <summary>
    ///     Function to get all hecontacts with pagination
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>The hecontacts</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the page or page size is less than 1</exception>
    /// <exception cref="ArgumentException">Thrown when the page size is greater than 100</exception>
    public async Task<IEnumerable<Hecontacts>> GetHecontactsAsync(int page, int pageSize)
    {
        if (page < 1) throw new ArgumentOutOfRangeException(nameof(page), "The page number cannot be less than 1");

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "The page size cannot be less than 1");

        return await _context.Hecontacts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(x => x.Hecreationdate)
            .ToListAsync();
    }

    /// <summary>
    ///     Function to get a single hecontact by its TIN (Tax Identification Number)
    /// </summary>
    /// <param name="tin">The TIN of the hecontact</param>
    /// <returns>The hecontact</returns>
    /// <exception cref="ArgumentNullException">Thrown when the TIN is null</exception>
    /// <exception cref="ArgumentException">Thrown when the TIN is empty</exception>
    public async Task<Hecontacts?> GetHecontactByTinAsync(string tin)
    {
        if (tin == null) throw new ArgumentNullException(nameof(tin));

        if (tin == string.Empty) throw new ArgumentException("The TIN cannot be empty", nameof(tin));

        return await _context.Hecontacts
            .Where(x => x.Hetin == tin)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Get all contacts , no pagination , no ordering
    /// </summary>
    /// <returns></returns>
    public async Task<List<Hecontacts>> GetAllContacts()
    {
        return await _context.Hecontacts.ToListAsync();
    }

    /// <summary>
    ///     Get contacts by name , no pagination , descending order by creation date
    /// </summary>
    /// <param name="name"></param>
    /// <returns>List of contacts</returns>
    public async Task<List<Hecontacts>> GetContactsByName(string name)
    {
        return await _context.Hecontacts
            .Where(x => x.Hename.ToUpper().Contains(name.ToUpper()) ||
                        x.Henamesoundex.ToUpper().Contains(name.ToUpper()))
            .OrderByDescending(x => x.Hecreationdate)
            .ToListAsync();
    }

    /// <summary>
    ///     Get contacts by phone number , no pagination , descending order by creation date
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>List of contacts</returns>
    public async Task<List<Hecontacts>> GetContactsByPhoneNumber(string phoneNumber)
    {
        return await _context.Hecontacts
            .Where(x => x.Hephone5 != null && x.Hephone4 != null && x.Hephone3 != null && x.Hephone2 != null &&
                        x.Hephone1 != null && (x.Hephone1.Contains(phoneNumber) || x.Hephone2.Contains(phoneNumber) ||
                                               x.Hephone3.Contains(phoneNumber) || x.Hephone4.Contains(phoneNumber) ||
                                               x.Hephone5.Contains(phoneNumber)))
            .OrderByDescending(x => x.Hecreationdate)
            .ToListAsync();
    }

    /// <summary>
    ///     Get contacts by email , no pagination , descending order by creation date
    /// </summary>
    /// <param name="email"></param>
    /// <returns>List of contacts</returns>
    public async Task<List<Hecontacts>> GetContactsByEmail(string email)
    {
        return await _context.Hecontacts
            .Where(x => x.Heemail1 != null && x.Heemail2 != null && x.Heemail3 != null && (x.Heemail1.Contains(email) ||
                x.Heemail2.Contains(email) ||
                x.Heemail3.Contains(email)))
            .OrderByDescending(x => x.Hecreationdate)
            .ToListAsync();
    }

    /// <summary>
    ///     Get count of contacts created in a given date range
    /// </summary>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public async Task<int> GetContactsCountByDateRange(DateTime fromDate, DateTime toDate)
    {
        return await _context.Hecontacts
            .Where(x => x.Hecreationdate >= fromDate && x.Hecreationdate <= toDate)
            .CountAsync();
    }
}