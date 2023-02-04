namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     This code defines a class called EdiProfileMap that implements the IEntityTypeConfiguration
///     <EdiProfile>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EdiProfileMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EdiProfile>
///             , which is used to build an entity type for the EdiProfile class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies whether certain properties are required or can be null.
/// </summary>
public class EdiProfileMap : IEntityTypeConfiguration<EdiProfile>
{
    public void Configure(EntityTypeBuilder<EdiProfile> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");
        builder.Property(c => c.Customer_Id)
            .HasColumnName("Customer_Id")
            .IsRequired();
        builder.Property(c => c.Model_Id)
            .HasColumnName("Model_Id")
            .IsRequired();
        builder.Property(c => c.Title)
            .HasColumnName("Title")
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            .IsRequired();
        builder.Property(c => c.Payload)
            .HasColumnName("Payload")
            .HasColumnType("TEXT")
            .IsRequired();
        builder.Property(c => c.Enabled)
            .HasColumnName("Enabled")
            .HasColumnType("bit")
            .IsRequired();
    }
}