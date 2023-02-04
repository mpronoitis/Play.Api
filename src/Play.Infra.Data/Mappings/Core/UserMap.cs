namespace Play.Infra.Data.Mappings.Core;

/// <summary>
///     This code defines a class called UserMap that implements the IEntityTypeConfiguration
///     <User>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         UserMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <User>
///             , which is used to build an entity type for the User class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies default values for certain properties and whether certain properties are required
///             or can be null.
/// </summary>
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.Id).HasColumnName("Id");

        //email
        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        //password , PBKDF2 hash
        builder.Property(c => c.PasswordHash)
            .HasColumnType("TEXT")
            .IsRequired();

        //password salt
        builder.Property(c => c.Salt)
            .HasColumnType("varchar(64)")
            .IsRequired();

        //UserName
        builder.Property(c => c.Username)
            .HasColumnType("varchar(100)")
            .IsRequired();

        //Role , default is Customer
        builder.Property(c => c.Role)
            .HasColumnType("varchar(100)")
            .HasDefaultValue("Customer")
            .IsRequired();

        //Login attempts 
        builder.Property(c => c.LoginAttempts)
            .HasColumnType("INT")
            .IsRequired();

        //failed login attempts
        builder.Property(c => c.FailedLoginAttempts)
            .HasColumnType("INT")
            .IsRequired();

        //last login ,can be null
        builder.Property(c => c.LastLogin)
            .HasColumnType("datetime");

        //otp 
        builder.Property(c => c.OtpSecret)
            .HasColumnType("varchar(100)");

        //CreatedAt , default value is CURRENT_TIMESTAMP
        builder.Property(c => c.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}