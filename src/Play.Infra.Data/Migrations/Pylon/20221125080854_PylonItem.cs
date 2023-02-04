#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Pylon;

/// <inheritdoc />
public partial class PylonItem : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "PylonItems",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Heid = table.Column<Guid>("uniqueidentifier", nullable: false),
                Code = table.Column<string>("varchar(100)", nullable: false),
                Name = table.Column<string>("varchar(100)", nullable: false),
                Description = table.Column<string>("varchar(100)", nullable: false),
                FactoryCode = table.Column<string>("varchar(100)", nullable: false),
                AuxiliaryCode = table.Column<string>("varchar(100)", nullable: false),
                Comments = table.Column<string>("varchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>("datetime", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_PylonItems", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "PylonItems");
    }
}