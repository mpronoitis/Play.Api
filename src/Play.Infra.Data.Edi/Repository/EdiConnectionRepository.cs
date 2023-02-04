namespace Play.Infra.Data.Edi.Repository;

public class EdiConnectionRepository : IEdiConnectionRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiConnection> DbSet;

    public EdiConnectionRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiConnection>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<EdiConnection> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<EdiConnection>> GetByModelIdAsync(Guid modelId)
    {
        return await DbSet.AsNoTracking().Where(x => x.Model_Id == modelId).ToListAsync();
    }

    public async Task<IEnumerable<EdiConnection>> GetByOrganizationIdAsync(Guid organizationId)
    {
        return await DbSet.AsNoTracking().Where(x => x.Org_Id == organizationId).ToListAsync();
    }

    public async Task<IEnumerable<EdiConnection>> GetByProfileIdAsync(Guid profileId)
    {
        return await DbSet.AsNoTracking().Where(x => x.Profile_Id == profileId).ToListAsync();
    }

    public async Task<IEnumerable<EdiConnection>> GetByCustomerIdAsync(Guid customerId)
    {
        return await DbSet.AsNoTracking().Where(x => x.Customer_Id == customerId).ToListAsync();
    }

    public async Task<IEnumerable<EdiConnection>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<EdiConnection>> GetAllByCustomerIdAsync(Guid customerId, int page = 1,
        int pageSize = 10)
    {
        return await DbSet.AsNoTracking().Where(x => x.Customer_Id == customerId).Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get total count
    /// </summary>
    public async Task<int> GetCountAsync()
    {
        return await DbSet.CountAsync();
    }

    //add 
    public void Add(EdiConnection ediConnection)
    {
        DbSet.Add(ediConnection);
    }

    //update
    public void Update(EdiConnection ediConnection)
    {
        DbSet.Update(ediConnection);
    }

    //delete
    public void Remove(EdiConnection ediConnection)
    {
        DbSet.Remove(ediConnection);
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}