namespace Play.Infra.Data.Edi.Repository;

public class EdiDocumentRepository : IEdiDocumentRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiDocument> DbSet;

    public EdiDocumentRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiDocument>();
    }

    public IUnitOfWork UnitOfWork => Db;

    //get by id async
    public async Task<EdiDocument> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    //get by title async
    public async Task<IEnumerable<EdiDocument>> GetByTitleAsync(string title)
    {
        return await DbSet.AsNoTracking().Where(x => x.Title == title).OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    //get by hedentid
    public async Task<IEnumerable<EdiDocument>> GetByHedentidAsync(string hedentid)
    {
        return await DbSet.AsNoTracking().Where(x => x.Hedentid == hedentid).OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    //get by IsProcessed
    public async Task<IEnumerable<EdiDocument>> GetByIsProcessedAsync(bool isProcessed)
    {
        return await DbSet.AsNoTracking().Where(x => x.IsProcessed == isProcessed).OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    public async Task<IEnumerable<EdiDocument>> GetByIsProcessedAndCustomerAsync(bool isProcessed, Guid customer)
    {
        return await DbSet.AsNoTracking().Where(x => x.IsProcessed == isProcessed && x.Customer_Id == customer)
            .OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    public async Task<IEnumerable<EdiDocument>> GetByIsSentAsync(bool isSent)
    {
        return await DbSet.AsNoTracking().Where(x => x.IsSent == isSent).OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    public async Task<IEnumerable<EdiDocument>> GetByIsSentAndCustomerIdAsync(bool isSent, Guid customerId)
    {
        return await DbSet.AsNoTracking().Where(x => x.IsSent == isSent && x.Customer_Id == customerId)
            .OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    //get by customer id
    public async Task<IEnumerable<EdiDocument>> GetByCustomerIdAsync(Guid customerId)
    {
        return await DbSet.AsNoTracking().Where(x => x.Customer_Id == customerId).OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }

    //get all with pagination by customer id
    public async Task<IEnumerable<EdiDocument>> GetAllWithPaginationByCustomerIdAsync(Guid customerId, int page = 1,
        int pageSize = 10)
    {
        return await DbSet.AsNoTracking().Where(x => x.Customer_Id == customerId).OrderByDescending(x => x.Created_At)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    //get all with pagination but no customer id
    public async Task<IEnumerable<EdiDocument>> GetAllWithPaginationAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().OrderByDescending(x => x.Created_At).Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    //get all with date range
    public async Task<IEnumerable<EdiDocument>> GetAllWithDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await DbSet.AsNoTracking().Where(x => x.Created_At >= startDate && x.Created_At <= endDate)
            .OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }


    //get all with date range and customer id
    public async Task<IEnumerable<EdiDocument>> GetAllWithDateRangeAndCustomerIdAsync(DateTime startDate,
        DateTime endDate, Guid customerId)
    {
        return await DbSet.AsNoTracking()
            .Where(x => x.Created_At >= startDate && x.Created_At <= endDate && x.Customer_Id == customerId)
            .OrderByDescending(x => x.Created_At)
            .ToListAsync();
    }
    
    //get all with no payloads and pagination
    public async Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAsync(int page = 1, int pageSize = 10)
    {
        //build query
        var result = await Db.EdiDocuments
            .Select(d => new { d.Id, d.Customer_Id, d.Title, d.IsProcessed, d.IsSent, d.Created_At })
            .OrderByDescending(d => d.Created_At)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        //map to EdiDocument
        var ediDocuments = result.Select(d => new EdiDocument
        {
            Id = d.Id,
            Customer_Id = d.Customer_Id,
            Title = d.Title,
            IsProcessed = d.IsProcessed,
            IsSent = d.IsSent,
            Created_At = d.Created_At
        }).ToList();
        
        return ediDocuments;
    }
    
    //get all with no payloads and pagination and customer id
    public async Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAndCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10)
    {
        //build query
        var result = await Db.EdiDocuments
            .Where(d => d.Customer_Id == customerId)
            .Select(d => new { d.Id, d.Customer_Id, d.Title, d.IsProcessed, d.IsSent, d.Created_At })
            .OrderByDescending(d => d.Created_At)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        //map to EdiDocument
        var ediDocuments = result.Select(d => new EdiDocument
        {
            Id = d.Id,
            Customer_Id = d.Customer_Id,
            Title = d.Title,
            IsProcessed = d.IsProcessed,
            IsSent = d.IsSent,
            Created_At = d.Created_At
        }).ToList();
        
        return ediDocuments;
    }
    
    /// <summary>
    ///  Get all with no payload and date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAndDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        //build query
        var result = await Db.EdiDocuments
            .Where(d => d.Created_At >= startDate && d.Created_At <= endDate)
            .Select(d => new { d.Id, d.Customer_Id, d.Title, d.IsProcessed, d.IsSent, d.Created_At })
            .OrderByDescending(d => d.Created_At)
            .ToListAsync();
        
        //map to EdiDocument
        var ediDocuments = result.Select(d => new EdiDocument
        {
            Id = d.Id,
            Customer_Id = d.Customer_Id,
            Title = d.Title,
            IsProcessed = d.IsProcessed,
            IsSent = d.IsSent,
            Created_At = d.Created_At
        }).ToList();
        
        return ediDocuments;
    }
    
    /// <summary>
    ///     Get all with no payload and date range and customer id
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="customerId"></param>
    public async Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAndDateRangeAndCustomerIdAsync(DateTime startDate, DateTime endDate, Guid customerId)
    {
        //build query
        var result = await Db.EdiDocuments
            .Where(d => d.Created_At >= startDate && d.Created_At <= endDate && d.Customer_Id == customerId)
            .Select(d => new { d.Id, d.Customer_Id, d.Title, d.IsProcessed, d.IsSent, d.Created_At })
            .OrderByDescending(d => d.Created_At)
            .ToListAsync();
        
        //map to EdiDocument
        var ediDocuments = result.Select(d => new EdiDocument
        {
            Id = d.Id,
            Customer_Id = d.Customer_Id,
            Title = d.Title,
            IsProcessed = d.IsProcessed,
            IsSent = d.IsSent,
            Created_At = d.Created_At
        }).ToList();
        
        return ediDocuments;
    }

    //get total count by customer id
    public async Task<int> GetTotalCountByCustomerIdAsync(Guid customerId)
    {
        return await DbSet.AsNoTracking().Where(x => x.Customer_Id == customerId).CountAsync();
    }

    /// <summary>
    ///     Get total count by customer id and date range
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<int> GetTotalCountByCustomerIdAndDateRangeAsync(Guid customerId, DateTime startDate,
        DateTime endDate)
    {
        return await DbSet.AsNoTracking()
            .Where(x => x.Customer_Id == customerId && x.Created_At >= startDate && x.Created_At <= endDate)
            .CountAsync();
    }

    //get total count
    public async Task<int> GetTotalCountAsync()
    {
        return await DbSet.AsNoTracking().CountAsync();
    }

    //add
    public void Register(EdiDocument ediDocument)
    {
        DbSet.Add(ediDocument);
    }

    //Update
    public void Update(EdiDocument ediDocument)
    {
        DbSet.Update(ediDocument);
    }

    //Update multiple
    public void UpdateMultiple(IEnumerable<EdiDocument> ediDocuments)
    {
        DbSet.UpdateRange(ediDocuments);
    }

    //Remove
    public void Remove(EdiDocument ediDocument)
    {
        DbSet.Remove(ediDocument);
    }

    //flush tracked changes
    public void Flush()
    {
        //remove all tracked changes
        Db.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}