namespace Play.Infra.Data.Mappings.Pylon;

public class PylonInvoiceMap : IEntityTypeConfiguration<PylonInvoice>
{
    public void Configure(EntityTypeBuilder<PylonInvoice> builder)
    {
        //id - guid
        builder.Property(c => c.Id).HasColumnName("Id");

        //invoice number - string
        builder.Property(c => c.InvoiceNumber).HasColumnName("InvoiceNumber").HasColumnType("varchar(100)");

        //invoice date - datetime
        builder.Property(c => c.InvoiceDate).HasColumnName("InvoiceDate").HasColumnType("datetime");

        //payment method - string
        builder.Property(c => c.PaymentMethod).HasColumnName("PaymentMethod").HasColumnType("varchar(100)");

        //total amount no tax - decimal
        builder.Property(c => c.TotalAmountNoTax).HasColumnName("TotalAmountNoTax").HasColumnType("decimal(18,2)");

        //total amount with tax - decimal
        builder.Property(c => c.TotalAmountWithTax).HasColumnName("TotalAmountWithTax").HasColumnType("decimal(18,2)");

        //total vat - decimal
        builder.Property(c => c.TotalVat).HasColumnName("TotalVat").HasColumnType("decimal(18,2)");

        //customer tin - string
        builder.Property(c => c.CustomerTin).HasColumnName("CustomerTin").HasColumnType("varchar(100)");

        //customer name - string
        builder.Property(c => c.CustomerName).HasColumnName("CustomerName").HasColumnType("varchar(100)");

        //vat regime - string
        builder.Property(c => c.VatRegime).HasColumnName("VatRegime").HasColumnType("varchar(100)");

        //eip url - string
        builder.Property(c => c.EipUrl).HasColumnName("EipUrl").HasColumnType("varchar(100)");

        //invoice lines - collection
        builder.OwnsMany(c => c.InvoiceLines, a =>
        {
            a.ToTable("PylonInvoiceLines");
            a.Property(c => c.Id).HasColumnName("Id");
            //item code
            a.Property(c => c.ItemCode).HasColumnName("ItemCode").HasColumnType("varchar(100)");
            //item name
            a.Property(c => c.ItemName).HasColumnName("ItemName").HasColumnType("varchar(100)");
            //item description
            a.Property(c => c.ItemDescription).HasColumnName("ItemDescription").HasColumnType("varchar(100)");
            //quantity
            a.Property(c => c.Quantity).HasColumnName("Quantity").HasColumnType("decimal(18,2)");
            //unit price
            a.Property(c => c.UnitPrice).HasColumnName("UnitPrice").HasColumnType("decimal(18,2)");
            //vat rate
            a.Property(c => c.VatRate).HasColumnName("VatRate").HasColumnType("decimal(18,2)");
            //total price
            a.Property(c => c.TotalPrice).HasColumnName("TotalPrice").HasColumnType("decimal(18,2)");
            //total vat
            a.Property(c => c.TotalVat).HasColumnName("TotalVat").HasColumnType("decimal(18,2)");
            //total price with vat
            a.Property(c => c.TotalPriceWithVat).HasColumnName("TotalPriceWithVat").HasColumnType("decimal(18,2)");
            //measurement unit
            a.Property(c => c.MeasurementUnit).HasColumnName("MeasurementUnit").HasColumnType("varchar(100)");
        });
    }
}