#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

public class AddLanguagePreference : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "LanguagePreference",
            "UserProfiles",
            "varchar(2)",
            maxLength: 2,
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "LanguagePreference",
            "UserProfiles");
    }
}