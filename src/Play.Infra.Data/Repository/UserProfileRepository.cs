namespace Play.Infra.Data.Repository;

public class UserProfileRepository : IUserProfileRepository
{
    protected readonly PlayCoreContext Db;
    protected readonly DbSet<UserProfile> DbSet;

    public UserProfileRepository(PlayCoreContext context)
    {
        Db = context;
        DbSet = Db.Set<UserProfile>();
    }

    public IUnitOfWork UnitOfWork => Db;

    //add
    public void Add(UserProfile user)
    {
        DbSet.Add(user);
    }

    //update
    public void Update(UserProfile user)
    {
        DbSet.Update(user);
    }

    //remove
    public void Remove(UserProfile user)
    {
        DbSet.Remove(user);
    }

    public async Task<UserProfile> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<UserProfile> GetByUserId(Guid user_id)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.User_Id == user_id);
    }

    /// <summary>
    ///     Get All User Profiles
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<UserProfile>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


    public void Dispose()
    {
        Db.Dispose();
    }
}