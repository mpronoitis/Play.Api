#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Pylon;

public partial class Initial : Migration
{
    //important!! support greek characters
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "PylonInvoices",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                InvoiceNumber = table.Column<string>("varchar(100)", nullable: false),
                InvoiceCode = table.Column<string>("varchar(100)", nullable: false),
                InvoiceDate = table.Column<DateTime>("datetime", nullable: false),
                PaymentMethod = table.Column<string>("varchar(100)", nullable: false),
                TotalAmountNoTax = table.Column<decimal>("decimal(18,2)", nullable: false),
                TotalAmountWithTax = table.Column<decimal>("decimal(18,2)", nullable: false),
                TotalVat = table.Column<decimal>("decimal(18,2)", nullable: false),
                CustomerTin = table.Column<string>("varchar(100)", nullable: false),
                CustomerName = table.Column<string>("varchar(100)", nullable: false),
                VatRegime = table.Column<string>("varchar(100)", nullable: false),
                EipUrl = table.Column<string>("varchar(100)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_PylonInvoices", x => x.Id); });

        migrationBuilder.CreateTable(
            "PylonInvoiceLines",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                PylonInvoiceId = table.Column<Guid>("uniqueidentifier", nullable: false),
                ItemCode = table.Column<string>("varchar(100)", nullable: false),
                ItemName = table.Column<string>("varchar(100)", nullable: false),
                ItemDescription = table.Column<string>("varchar(100)", nullable: false),
                Quantity = table.Column<decimal>("decimal(18,2)", nullable: false),
                UnitPrice = table.Column<decimal>("decimal(18,2)", nullable: false),
                VatRate = table.Column<decimal>("decimal(18,2)", nullable: false),
                TotalPrice = table.Column<decimal>("decimal(18,2)", nullable: false),
                TotalVat = table.Column<decimal>("decimal(18,2)", nullable: false),
                TotalPriceWithVat = table.Column<decimal>("decimal(18,2)", nullable: false),
                MeasurementUnit = table.Column<string>("varchar(100)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PylonInvoiceLines", x => new { x.PylonInvoiceId, x.Id });
                table.ForeignKey(
                    "FK_PylonInvoiceLines_PylonInvoices_PylonInvoiceId",
                    x => x.PylonInvoiceId,
                    "PylonInvoices",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "PylonInvoiceLines");

        migrationBuilder.DropTable(
            "PylonInvoices");
    }
}