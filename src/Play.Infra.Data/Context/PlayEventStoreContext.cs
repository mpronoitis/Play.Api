using Play.Domain.EventSourcing.Models;
using Play.Infra.Data.Mappings.Events;

namespace Play.Infra.Data.Context;

public class PlayEventStoreContext : DbContext
{
    
    public PlayEventStoreContext(DbContextOptions<PlayEventStoreContext> options) : base(options)
    {
    }
    public DbSet<StoredEvent> StoredEvent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new StoredEventMap());
        base.OnModelCreating(modelBuilder);
    }
}