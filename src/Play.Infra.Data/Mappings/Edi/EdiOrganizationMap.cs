namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     This code defines a class called EdiOrganizationMap that implements the IEntityTypeConfiguration
///     <EdiOrganization>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EdiOrganizationMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EdiOrganization>
///             , which is used to build an entity type for the EdiOrganization class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies whether certain properties are required or can be null.
/// </summary>
public class EdiOrganizationMap : IEntityTypeConfiguration<EdiOrganization>
{
    public void Configure(EntityTypeBuilder<EdiOrganization> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}