namespace Play.Infra.Data.Mappings.Pylon;

public class PylonContactMap : IEntityTypeConfiguration<PylonContact>
{
    public void Configure(EntityTypeBuilder<PylonContact> builder)
    {
        //id pk
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(c => c.Heid)
            .HasColumnName("PylonId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        //Code
        builder.Property(c => c.Code)
            .HasColumnName("Code")
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        //Name
        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        //FirstName
        builder.Property(c => c.FirstName)
            .HasColumnName("FirstName")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        //LastName
        builder.Property(c => c.LastName)
            .HasColumnName("LastName")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        //TIN
        builder.Property(c => c.Tin)
            .HasColumnName("TIN")
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        //Emails
        builder.Property(c => c.Emails)
            .HasColumnName("Emails")
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();

        //Phones
        builder.Property(c => c.Phones)
            .HasColumnName("Phones")
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();

        //Address
        builder.Property(c => c.Address)
            .HasColumnName("Address")
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();

        //CreatedDate
        builder.Property(c => c.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("datetime")
            .IsRequired();
    }
}