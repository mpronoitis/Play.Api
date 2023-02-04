#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

public class AddOtpToUser : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "OtpSecret",
            "Users",
            "varchar(100)",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "OtpSecret",
            "Users");
    }
}