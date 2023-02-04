namespace Play.Infra.Data.Mappings.Core;

/// <summary>
///     This code defines a class called UserProfileMap that implements the IEntityTypeConfiguration
///     <UserProfile>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         UserProfileMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <UserProfile>
///             , which is used to build an entity type for the UserProfile class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies default values for certain properties and whether certain properties are required
///             or can be null.
/// </summary>
public class UserProfileMap : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.User_Id)
            .HasColumnName("User_Id")
            .IsRequired();

        builder.Property(c => c.FirstName)
            .HasColumnName("FirstName")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.LastName)
            .HasColumnName("LastName")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.DateOfBirth)
            .HasColumnName("DateOfBirth")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(c => c.CompanyName)
            .HasColumnName("CompanyName")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.LanguagePreference)
            .HasColumnName("LanguagePreference")
            .HasColumnType("varchar(2)")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(c => c.ThemePreference)
            .HasColumnName("ThemePreference")
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.TIN)
            .HasColumnName("TIN")
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .HasDefaultValue("0");
    }
}