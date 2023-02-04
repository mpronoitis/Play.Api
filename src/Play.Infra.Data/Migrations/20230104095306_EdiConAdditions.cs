using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Play.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EdiConAdditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File_Type",
                table: "EdiConnections",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Ftp_Port",
                table: "EdiConnections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File_Type",
                table: "EdiConnections");

            migrationBuilder.DropColumn(
                name: "Ftp_Port",
                table: "EdiConnections");
        }
    }
}
