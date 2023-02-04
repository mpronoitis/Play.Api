namespace Play.Infra.Data.Edi.Repository;

public class EdiSegmentRepository : IEdiSegmentRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiSegment> DbSet;

    public EdiSegmentRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiSegment>();
    }

    public IUnitOfWork UnitOfWork => Db;

    //get by id async
    public async Task<EdiSegment> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    //get by model id
    public async Task<EdiSegment> GetByModelIdAsync(Guid modelId)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Model_Id == modelId);
    }

    //get all with paging
    public async Task<IEnumerable<EdiSegment>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get total count of records
    /// </summary>
    /// <returns> Total count of records </returns>
    public async Task<int> GetCountAsync()
    {
        return await DbSet.CountAsync();
    }

    //add
    public void Add(EdiSegment model)
    {
        DbSet.Add(model);
    }

    public void AddRange(IEnumerable<EdiSegment> models)
    {
        DbSet.AddRange(models);
    }

    //update
    public void Update(EdiSegment model)
    {
        DbSet.Update(model);
    }

    //delete
    public void Delete(EdiSegment model)
    {
        DbSet.Remove(model);
    }

    public void DeleteRange(IEnumerable<EdiSegment> models)
    {
        DbSet.RemoveRange(models);
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