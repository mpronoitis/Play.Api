namespace Play.Infra.Data.Repository;

public class EmailTemplateRepository : IEmailTemplateRepository
{
    protected readonly PlayCoreContext Db;
    protected readonly DbSet<EmailTemplate> DbSet;

    public EmailTemplateRepository(PlayCoreContext context)
    {
        Db = context;
        DbSet = Db.Set<EmailTemplate>();
    }

    public IUnitOfWork UnitOfWork => Db;

    /// <summary>
    ///     Find by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<EmailTemplate> FindByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    ///     Get all templates with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>List of templates</returns>
    public async Task<List<EmailTemplate>> GetAllAsync(int page, int pageSize)
    {
        return await DbSet
            .OrderByDescending(x => x.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    ///     Get by Subject
    /// </summary>
    /// <param name="subject">Subject</param>
    /// <returns>List of templates</returns>
    public async Task<List<EmailTemplate>> GetBySubjectAsync(string subject)
    {
        return await DbSet
            .Where(x => x.Subject.ToUpper().Contains(subject.ToUpper()))
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }

    /// <summary>
    ///     Get by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>List of templates</returns>
    public async Task<List<EmailTemplate>> GetByNameAsync(string name)
    {
        return await DbSet
            .Where(x => x.Name.ToUpper().Contains(name.ToUpper()))
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }

    /// <summary>
    ///     Add new template
    /// </summary>
    /// <param name="template">Template</param>
    /// <returns> void </returns>
    public async Task AddAsync(EmailTemplate template)
    {
        await DbSet.AddAsync(template);
    }

    /// <summary>
    ///     Update template
    /// </summary>
    /// <param name="template">Template</param>
    /// <returns> void </returns>
    public void Update(EmailTemplate template)
    {
        DbSet.Update(template);
    }

    /// <summary>
    ///     Delete template
    /// </summary>
    /// <param name="template">Template</param>
    /// <returns> void </returns>
    public void Delete(EmailTemplate template)
    {
        DbSet.Remove(template);
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}