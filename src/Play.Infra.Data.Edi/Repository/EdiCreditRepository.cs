namespace Play.Infra.Data.Edi.Repository;

public class EdiCreditRepository : IEdiCreditRepository
{
    private readonly PlayContext Db;
    private readonly DbSet<EdiCredit> DbSet;

    public EdiCreditRepository(PlayContext context)
    {
        Db = context;
        DbSet = Db.Set<EdiCredit>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<EdiCredit> GetByCreditIdAsync(Guid creditId)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == creditId);
    }

    public async Task<EdiCredit> GetByCustomerIdAsync(Guid customerId)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<IEnumerable<EdiCredit>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<EdiCredit>> GetAllWithPagingAsync(int page = 1, int pagesize = 10)
    {
        return await DbSet.AsNoTracking().OrderByDescending(c => c.CreatedAt).Skip(page - 1).Take(pagesize)
            .ToListAsync();
    }
    
    public async Task DecrementCreditAsync(Guid CustomerId, int amount)
    {
        var credit = await DbSet.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
        //if credit is null, return
        if (credit == null)
            return;
        credit.Amount -= amount;
        DbSet.Update(credit);
    }

    public void Add(EdiCredit credit)
    {
        DbSet.Add(credit);
    }

    public void Update(EdiCredit credit)
    {
        DbSet.Update(credit);
    }

    public void Remove(EdiCredit credit)
    {
        DbSet.Remove(credit);
    }
}