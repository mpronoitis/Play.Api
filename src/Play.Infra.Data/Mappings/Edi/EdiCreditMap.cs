namespace Play.Infra.Data.Mappings.Edi;

/// <summary>
///     This code defines a class called EdiCreditMap that implements the IEntityTypeConfiguration
///     <EdiCredit>
///         interface.
///         This interface requires the implementation of a Configure method, which is what the Configure method in the
///         EdiCreditMap class is.
///         The Configure method takes a parameter of type EntityTypeBuilder
///         <EdiCredit>
///             , which is used to build an entity type for the EdiCredit class.
///             The method sets the name of the database table that the entity will be mapped to as "EdiCredits" and sets
///             the primary key for the entity to be the Id property.
///             The method also defines several properties on the entity, including the column names and data types in the
///             database table and whether certain properties are required or can be null.
/// </summary>
public class EdiCreditMap : IEntityTypeConfiguration<EdiCredit>
{
    public void Configure(EntityTypeBuilder<EdiCredit> builder)
    {
        //table name EdiCredits
        builder.ToTable("EdiCredits");

        //primary key (id guid)
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();

        //foreign key (customer id)
        builder.Property(x => x.CustomerId).HasColumnName("CustomerId").HasColumnType("uniqueidentifier").IsRequired();

        //ammount (integer)
        builder.Property(x => x.Amount).HasColumnName("Amount").HasColumnType("int").IsRequired();

        //updated at (datetime)
        builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("datetime").IsRequired();

        //created at (datetime)
        builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime").IsRequired();
    }
}