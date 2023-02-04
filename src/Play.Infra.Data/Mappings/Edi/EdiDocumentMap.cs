namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     This code defines a class called EdiDocumentMap that implements the IEntityTypeConfiguration
///     <EdiDocument>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EdiDocumentMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EdiDocument>
///             , which is used to build an entity type for the EdiDocument class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies default values for certain properties and whether certain properties are required
///             or can be null.
/// </summary>
public class EdiDocumentMap : IEntityTypeConfiguration<EdiDocument>
{
    public void Configure(EntityTypeBuilder<EdiDocument> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Customer_Id)
            .HasColumnName("Customer_Id")
            .IsRequired();

        builder.Property(c => c.Title)
            .HasColumnName("Title")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(c => c.EdiPayload)
            .HasColumnName("EdiPayload")
            .HasColumnType("text");

        builder.Property(c => c.DocumentPayload)
            .HasColumnName("DocumentPayload")
            .HasColumnType("text");

        builder.Property(c => c.Hedentid)
            .HasColumnName("Hedentid")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.IsProcessed)
            .HasColumnName("IsProcessed")
            .HasDefaultValue(false);

        builder.Property(c => c.IsSent)
            .HasColumnName("IsSent")
            .HasDefaultValue(false);

        builder.Property(c => c.Created_At)
            .HasColumnName("Created_At")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}