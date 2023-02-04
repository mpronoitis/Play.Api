namespace Play.Infra.Data.Context;

/// <summary>
///     The PlayContext contains tables related to the Play domain , which means anything not related to Pylon and Identity
///     are in this context.
///     Example: Edi,UptimeKuma,Contracting
/// </summary>
public sealed class PlayContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public PlayContext(DbContextOptions<PlayContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    //Edi 
    public DbSet<EdiOrganization> EdiOrganizations { get; set; }
    public DbSet<EdiModel> EdiModels { get; set; }
    public DbSet<EdiProfile> EdiProfiles { get; set; }
    public DbSet<EdiConnection> EdiConnections { get; set; }
    public DbSet<EdiSegment> EdiSegments { get; set; }
    public DbSet<EdiVariable> EdiVariables { get; set; }
    public DbSet<EdiCredit> EdiCredits { get; set; }

    public DbSet<EdiDocument> EdiDocuments { get; set; }

    //Kuma
    public DbSet<KumaNotification> KumaNotifications { get; set; }

    //Contracting
    public DbSet<Contract> Contracts { get; set; }


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

        modelBuilder.ApplyConfiguration(new EdiOrganizationMap());
        modelBuilder.ApplyConfiguration(new EdiModelMap());
        modelBuilder.ApplyConfiguration(new EdiProfileMap());
        modelBuilder.ApplyConfiguration(new EdiConnectionMap());
        modelBuilder.ApplyConfiguration(new EdiSegmentMap());
        modelBuilder.ApplyConfiguration(new EdiVariableMap());
        modelBuilder.ApplyConfiguration(new EdiDocumentMap());
        modelBuilder.ApplyConfiguration(new EdiCreditMap());
        //kuma
        modelBuilder.ApplyConfiguration(new KumaNotificationMap());
        //Contracting
        modelBuilder.ApplyConfiguration(new ContractMap());


        base.OnModelCreating(modelBuilder);
    }
}