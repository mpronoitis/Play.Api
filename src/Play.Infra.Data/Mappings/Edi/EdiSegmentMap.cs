namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     The IEntityTypeConfiguration
///     <TEntity>
///         interface requires that the class implement a Configure method, which takes in an EntityTypeBuilder
///         <TEntity>
///             object and sets up the configuration for the entity.
///             The EntityTypeBuilder
///             <TEntity>
///                 object is used to define the shape of the entity and how it maps to the database table.
///                 For example, in the ContractMap class, the Id property of the Contract entity is defined as the primary
///                 key of the Contract table and is mapped to the Id column,
///                 which has the uniqueidentifier data type and is required. Similarly, the other properties of the
///                 Contract entity are mapped to their corresponding columns in the Contract table,
///                 with their data types and other constraints specified.
/// </summary>
public class EdiSegmentMap : IEntityTypeConfiguration<EdiSegment>
{
    public void Configure(EntityTypeBuilder<EdiSegment> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Model_Id)
            .HasColumnName("Model_Id")
            .IsRequired();

        builder.Property(c => c.Title)
            .HasColumnName("Title")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("Description")
            .HasMaxLength(500);
    }
}