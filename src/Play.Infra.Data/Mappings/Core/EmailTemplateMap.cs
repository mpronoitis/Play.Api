namespace Play.Infra.Data.Mappings.Core;

/// <summary>
///     This code defines a class called EmailTemplateMap that implements the IEntityTypeConfiguration
///     <EmailTemplate>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EmailTemplateMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EmailTemplate>
///             , which is used to build an entity type for the EmailTemplate class.
///             The method sets the primary key for the EmailTemplate entity to be the Id property and defines several
///             properties on the entity,
///             including the column names and data types in the database table that the entity will be mapped to.
/// </summary>
public class EmailTemplateMap : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        // Primary Key (id)
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("Id");
        //name
        builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
        //subject
        builder.Property(t => t.Subject).HasColumnName("Subject").HasMaxLength(100).IsRequired();
        //body BIGTEXT
        builder.Property(t => t.Body).HasColumnName("Body").HasColumnType("NVARCHAR(MAX)").IsRequired();
        //createdDate
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();
    }
}