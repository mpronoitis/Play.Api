namespace Play.Infra.Data.Context;

/// <summary>
///     This context contains Temp Pylon Tables such as PylonInvoices, PylonContacts , PylonItems
///     This has been done because fetching data from Pylon is very slow and we need to cache it in our own database
///     ¯\_(ツ)_/¯
/// </summary>
public sealed class PlayPylonContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public PlayPylonContext(DbContextOptions<PlayPylonContext> options, IMediatorHandler mediatorHandler) :
        base(options)
    {
        //set injected mediatorHandler
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    //db sets
    public DbSet<PylonInvoice> PylonInvoices { get; set; }
    public DbSet<PylonItem> PylonItems { get; set; }
    public DbSet<PylonContact> PylonContacts { get; set; }

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
        modelBuilder.ApplyConfiguration(new PylonInvoiceMap());
        modelBuilder.ApplyConfiguration(new PylonItemMap());
        modelBuilder.ApplyConfiguration(new PylonContactMap());

        base.OnModelCreating(modelBuilder);
    }
}