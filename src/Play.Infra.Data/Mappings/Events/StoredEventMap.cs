using Play.Domain.EventSourcing.Models;

namespace Play.Infra.Data.Mappings.Events;

public class StoredEventMap: IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        //id 
        builder.Property(c => c.Id).HasColumnName("Id");
        
        //aggregate id
        builder.Property(c => c.AggregateId).HasColumnName("AggregateId");
        
        //data
        builder.Property(c => c.Data).HasColumnName("Data");
        
        //message type
        builder.Property(c => c.MessageType).HasColumnName("MessageType");
        
        //timestamp
        builder.Property(c => c.Timestamp).HasColumnName("Timestamp");
        
    }
}