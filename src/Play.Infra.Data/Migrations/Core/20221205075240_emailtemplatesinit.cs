#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

/// <inheritdoc />
public partial class emailtemplatesinit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "EmailTemplates",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Name = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                Subject = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                Body = table.Column<string>("varchar(8000)", maxLength: 8000, nullable: false),
                CreatedDate = table.Column<DateTime>("datetime2", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_EmailTemplates", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "EmailTemplates");
    }
}