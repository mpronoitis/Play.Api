#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

/// <inheritdoc />
#pragma warning disable CS8981
public partial class emailtemplatesupdate : Migration
#pragma warning restore CS8981
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            "Body",
            "EmailTemplates",
            "NVARCHAR(MAX)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "BIGTEXT");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            "Body",
            "EmailTemplates",
            "BIGTEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "NVARCHAR(MAX)");
    }
}