namespace Play.Infra.Data.Edi.Repository;

public class EdiOrganizationRepository : IEdiOrganizationRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiOrganization> DbSet;

    /// <summary>
    ///     Constructor, inject the Context
    /// </summary>
    /// <param name="db"></param>
    public EdiOrganizationRepository(PlayContext db)
    {
        Db = db;
        DbSet = Db.Set<EdiOrganization>();
    }

    //we will use unitOfWork pattern to make sure that controllers make use of same context
    public IUnitOfWork UnitOfWork => Db;

    public async Task<EdiOrganization> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<EdiOrganization> GetByEmailAsync(string email)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }

    /// <summary>
    ///     GetAll function , allows for pagination
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<EdiOrganization>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }


    public void Add(EdiOrganization ediOrganization)
    {
        DbSet.Add(ediOrganization);
    }

    public void Update(EdiOrganization ediOrganization)
    {
        DbSet.Update(ediOrganization);
    }

    public void Remove(EdiOrganization ediOrganization)
    {
        DbSet.Remove(ediOrganization);
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
}