namespace Play.Infra.Data.Edi.Repository;

public class EdiVariableRepository : IEdiVariableRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiVariable> DbSet;

    public EdiVariableRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiVariable>();
    }

    //unit of work
    public IUnitOfWork UnitOfWork => Db;

    //get by id async
    public async Task<EdiVariable> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    //get by placeholder
    public async Task<IEnumerable<EdiVariable>> GetByPlaceholderAsync(string placeholder)
    {
        return await DbSet.AsNoTracking().Where(x => x.Placeholder == placeholder).ToListAsync();
    }

    //get by title
    public async Task<IEnumerable<EdiVariable>> GetByTitleAsync(string title)
    {
        return await DbSet.AsNoTracking().Where(x => x.Title == title).ToListAsync();
    }

    //get all async , with paging
    public async Task<IEnumerable<EdiVariable>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get count of all records
    /// </summary>
    /// <returns> Count of all records </returns>
    public async Task<int> CountAllAsync()
    {
        return await DbSet.CountAsync();
    }

    //add
    public void Register(EdiVariable ediVariable)
    {
        DbSet.Add(ediVariable);
    }

    //update
    public void Update(EdiVariable ediVariable)
    {
        DbSet.Update(ediVariable);
    }

    //remove
    public void Remove(EdiVariable ediVariable)
    {
        DbSet.Remove(ediVariable);
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