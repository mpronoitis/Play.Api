using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Play.Infra.Data.Migrations
{
    public partial class AddEdiConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EdiConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Org_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Profile_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ftp_Hostname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Ftp_Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Ftp_Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdiConnections", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EdiConnections");
        }
    }
}
