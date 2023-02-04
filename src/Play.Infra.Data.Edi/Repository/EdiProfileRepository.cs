namespace Play.Infra.Data.Edi.Repository;

public class EdiProfileRepository : IEdiProfileRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiProfile> DbSet;

    public EdiProfileRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiProfile>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<EdiProfile> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<EdiProfile>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<EdiProfile>> GetAllByCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking()
            .Where(x => x.Customer_Id == customerId)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<EdiProfile> GetByModelIdAsync(Guid modelId)
    {
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Model_Id == modelId);
    }

    public async Task<IEnumerable<EdiProfile>> GetByUserIdAsync(Guid userId)
    {
        return await DbSet.AsNoTracking()
            .Where(x => x.Customer_Id == userId)
            .ToListAsync();
    }

    /// <summary>
    ///     Get total count of records
    /// </summary>
    /// <returns> Total count of records </returns>
    public async Task<int> GetCountAsync()
    {
        return await DbSet.CountAsync();
    }


    public void Add(EdiProfile ediProfile)
    {
        DbSet.Add(ediProfile);
    }

    public void Update(EdiProfile ediProfile)
    {
        DbSet.Update(ediProfile);
    }

    public void Remove(EdiProfile ediProfile)
    {
        DbSet.Remove(ediProfile);
    }

    public void Dispose()
    {
        Db.Dispose();
    }

    //flush tracked changes
    public void Flush()
    {
        //remove all tracked changes
        Db.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);
    }

    //remove all
    public void RemoveAll()
    {
        //remove all tracked changes
        Db.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);
        //remove all records
        DbSet.RemoveRange(DbSet);
    }
}