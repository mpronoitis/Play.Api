namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     This code defines a class called EdiModelMap that implements the IEntityTypeConfiguration
///     <EdiModel>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EdiModelMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EdiModel>
///             , which is used to build an entity type for the EdiModel class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies whether certain properties are required or can be null.
/// </summary>
public class EdiModelMap : IEntityTypeConfiguration<EdiModel>
{
    public void Configure(EntityTypeBuilder<EdiModel> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Org_Id)
            .HasColumnName("Org_Id");

        builder.Property(c => c.Title)
            .HasColumnName("Title")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.SegmentTerminator)
            .HasColumnName("SegmentTerminator")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(c => c.SubElementSeparator)
            .HasColumnName("SubElementSeparator")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(c => c.ElementSeparator)
            .HasColumnName("ElementSeparator")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(c => c.Enabled)
            .HasColumnName("Enabled")
            .IsRequired();
    }
}