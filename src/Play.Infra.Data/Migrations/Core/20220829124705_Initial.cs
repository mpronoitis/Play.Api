#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Play.Infra.Data.Migrations.Core;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Email = table.Column<string>("varchar(100)", nullable: false),
                PasswordHash = table.Column<string>("TEXT", nullable: false),
                Salt = table.Column<string>("varchar(64)", nullable: false),
                Username = table.Column<string>("varchar(100)", nullable: false),
                Role = table.Column<string>("varchar(100)", nullable: false, defaultValue: "Customer"),
                LoginAttempts = table.Column<int>("INT", nullable: false),
                FailedLoginAttempts = table.Column<int>("INT", nullable: false),
                LastLogin = table.Column<DateTime>("datetime", nullable: true),
                CreatedAt = table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Users");
    }
}