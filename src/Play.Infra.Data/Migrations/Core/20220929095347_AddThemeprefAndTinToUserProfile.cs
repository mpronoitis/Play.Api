#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

public partial class AddThemeprefAndTinToUserProfile : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            "OtpSecret",
            "Users",
            "varchar(100)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldNullable: true,
            oldDefaultValueSql: "uniqueidentifier()");

        migrationBuilder.AddColumn<string>(
            "TIN",
            "UserProfiles",
            "varchar(20)",
            maxLength: 20,
            nullable: true,
            defaultValue: "0");

        migrationBuilder.AddColumn<string>(
            "ThemePreference",
            "UserProfiles",
            "varchar(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "TIN",
            "UserProfiles");

        migrationBuilder.DropColumn(
            "ThemePreference",
            "UserProfiles");

        migrationBuilder.AlterColumn<string>(
            "OtpSecret",
            "Users",
            "varchar(100)",
            nullable: true,
            defaultValueSql: "uniqueidentifier()",
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldNullable: true);
    }
}