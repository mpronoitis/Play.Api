#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Pylon;

/// <inheritdoc />
public partial class PylonContactInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "PylonContacts",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                PylonId = table.Column<Guid>("uniqueidentifier", nullable: false),
                Code = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                Name = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                FirstName = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                TIN = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                Emails = table.Column<string>("varchar(500)", maxLength: 500, nullable: false),
                Phones = table.Column<string>("varchar(500)", maxLength: 500, nullable: false),
                Address = table.Column<string>("varchar(500)", maxLength: 500, nullable: false),
                CreatedDate = table.Column<DateTime>("datetime", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_PylonContacts", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "PylonContacts");
    }
}