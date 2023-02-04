namespace Play.Infra.Data.Context;

/// <summary>
///     This context contains , strictly , Users , UserProfiles, EmailTemplates
/// </summary>
public sealed class PlayCoreContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public PlayCoreContext(DbContextOptions<PlayCoreContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        //set injected mediatorHandler
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    //db sets
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }

    public async Task<bool> Commit()
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;

        return success;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        //apply maps
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new UserProfileMap());
        modelBuilder.ApplyConfiguration(new EmailTemplateMap());

        base.OnModelCreating(modelBuilder);
    }
}