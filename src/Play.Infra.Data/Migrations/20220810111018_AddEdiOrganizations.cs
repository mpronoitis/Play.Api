using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Play.Infra.Data.Migrations
{
    public partial class AddEdiOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //create new table called EdiOrganizations based on EdiOrganization model
            migrationBuilder.CreateTable(
                name: "EdiOrganizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                   
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdiOrganizations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //drop table EdiOrganizations
            migrationBuilder.DropTable(
                name: "EdiOrganizations");

        }
    }
}
