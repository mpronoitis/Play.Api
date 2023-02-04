namespace Play.Infra.Data.Edi.Repository;

public class EdiModelRepository : IEdiModelRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiModel> DbSet;

    public EdiModelRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiModel>();
    }

    //we will use unitOfWork pattern to make sure that controllers make use of same context
    public IUnitOfWork UnitOfWork => Db;

    //get by id async
    public async Task<EdiModel> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    //get all async allows for pagination
    public async Task<IEnumerable<EdiModel>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().OrderBy(x => x.Title).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    //get by title
    public async Task<EdiModel> GetByTitleAsync(string title)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
    }

    /// <summary>
    ///     Get total count of records
    /// </summary>
    /// <returns> Total count of records </returns>
    public async Task<int> GetTotalCountAsync()
    {
        return await DbSet.CountAsync();
    }

    //add
    public void Add(EdiModel ediModel)
    {
        DbSet.Add(ediModel);
    }

    //update
    public void Update(EdiModel ediModel)
    {
        DbSet.Update(ediModel);
    }

    //remove
    public void Remove(EdiModel model)
    {
        DbSet.Remove(model);
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