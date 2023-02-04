namespace Play.Infra.Data.Mappings.Pylon;

public class PylonItemMap : IEntityTypeConfiguration<PylonItem>
{
    public void Configure(EntityTypeBuilder<PylonItem> builder)
    {
        //id guid
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        //heid guid
        builder.Property(p => p.Heid)
            .HasColumnName("Heid")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        //name string
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //code string
        builder.Property(p => p.Code)
            .HasColumnName("Code")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //description string
        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //factory code string
        builder.Property(p => p.FactoryCode)
            .HasColumnName("FactoryCode")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //Auxiliary code string
        builder.Property(p => p.AuxiliaryCode)
            .HasColumnName("AuxiliaryCode")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //Comments (max string)
        builder.Property(p => p.Comments)
            .HasColumnName("Comments")
            .HasColumnType("varchar(max)")
            .IsRequired();

        //Creation date
        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired();
    }
}