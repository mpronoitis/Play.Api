#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

public partial class AddUserProfile : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "UserProfiles",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                User_Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                FirstName = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                DateOfBirth = table.Column<DateTime>("datetime", nullable: false),
                CompanyName = table.Column<string>("varchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_UserProfiles", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "UserProfiles");
    }
}