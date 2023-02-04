namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     For example, the EmailTemplateMap class specifies that the EmailTemplate entity has a primary key column named Id,
///     a required column named Name with a maximum length of 100 characters,
///     a required column named Subject with a maximum length of 100 characters, a required column named Body of type
///     NVARCHAR(MAX), and a required column named CreatedDate.
///     Similarly, the UserMap class specifies that the User entity has a primary key column named Id, a required column
///     named Email of type varchar(100),
///     a required column named PasswordHash of type TEXT, a required column named Salt of type varchar(64), a required
///     column named Username of type varchar(100),
///     a required column named Role of type varchar(100) with a default value of "Customer", etc.
///     These Entity Type Configuration classes are used by Entity Framework to set up the database schema when the
///     application runs for the first time,
///     or when the database is being migrated to a new version. They can also be used to customize various aspects of the
///     database mapping, such as column names, data types, default values, and so on.
/// </summary>
public class EdiVariableMap : IEntityTypeConfiguration<EdiVariable>
{
    public void Configure(EntityTypeBuilder<EdiVariable> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Title)
            .HasColumnName("Title")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("Description")
            .HasMaxLength(500);

        builder.Property(c => c.Placeholder)
            .HasColumnName("Placeholder")
            .HasMaxLength(50);
    }
}