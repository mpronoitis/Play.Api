namespace Play.Infra.Data.Mappings.Contracts;

/// <summary>
///     This code defines a class called ContractMap that implements the IEntityTypeConfiguration
///     <Contract>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         ContractMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <Contract>
///             , which is used to build an entity type for the Contract class.
///             The method sets the primary key for the Contract entity to be the Id property and defines several
///             properties on the entity,
///             including the column names and data types in the database table that the entity will be mapped to.
/// </summary>
public class ContractMap : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        //the id is the primary key
        builder.HasKey(c => c.Id);
        //the id column
        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        //contract code
        builder.Property(c => c.Code)
            .HasColumnName("ContractCode")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //The client's name
        builder.Property(c => c.ClientName)
            .HasColumnName("ClientName")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //the client's tin
        builder.Property(c => c.ClientTin)
            .HasColumnName("ClientTin")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //the item's name
        builder.Property(c => c.ItemName)
            .HasColumnName("ItemName")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //the status of the contract
        builder.Property(c => c.Status)
            .HasColumnName("Status")
            .HasColumnType("varchar(100)")
            .IsRequired();

        //The startDate of the contract
        builder.Property(c => c.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("datetime")
            .IsRequired();

        //the endDate of the contract
        builder.Property(c => c.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("datetime")
            .IsRequired();

        //the createdAt of the contract
        builder.Property(c => c.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired();

        //the guid of the client
        builder.Property(c => c.ClientId)
            .HasColumnName("ClientId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        //the guid of the item
        builder.Property(c => c.ItemId)
            .HasColumnName("ItemId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
    }
}