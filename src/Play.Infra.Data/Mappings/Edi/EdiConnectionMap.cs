namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     This code defines a class called EdiConnectionMap that implements the IEntityTypeConfiguration
///     <EdiConnection>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EdiConnectionMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EdiConnection>
///             , which is used to build an entity type for the EdiConnection class.
///             The method defines several properties on the entity, including the column names and data types in the
///             database table that the entity will be mapped to.
///             The method also specifies whether certain properties are required or can be null.
/// </summary>
public class EdiConnectionMap : IEntityTypeConfiguration<EdiConnection>
{
    public void Configure(EntityTypeBuilder<EdiConnection> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Customer_Id)
            .HasColumnName("Customer_Id")
            .IsRequired();

        builder.Property(c => c.Model_Id)
            .HasColumnName("Model_Id")
            .IsRequired();

        builder.Property(c => c.Org_Id)
            .HasColumnName("Org_Id")
            .IsRequired();

        builder.Property(c => c.Profile_Id)
            .HasColumnName("Profile_Id")
            .IsRequired();

        builder.Property(c => c.Ftp_Hostname)
            .HasColumnName("Ftp_Hostname")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Ftp_Username)
            .HasColumnName("Ftp_Username")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Ftp_Password)
            .HasColumnName("Ftp_Password")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(c => c.Ftp_Port)
            .HasColumnName("Ftp_Port")
            .IsRequired();
        
        builder.Property(c => c.File_Type)
            .HasColumnName("File_Type")
            .HasMaxLength(100)
            .IsRequired();
    }
}