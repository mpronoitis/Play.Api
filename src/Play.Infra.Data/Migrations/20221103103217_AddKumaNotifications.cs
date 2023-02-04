using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Play.Infra.Data.Migrations
{
    public partial class AddKumaNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KumaNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    msg = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KumaNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KumaHeartbeat",
                columns: table => new
                {
                    KumaNotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonitorID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "varchar(100)", nullable: false),
                    Msg = table.Column<string>(type: "varchar(100)", nullable: false),
                    Important = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KumaHeartbeat", x => x.KumaNotificationId);
                    table.ForeignKey(
                        name: "FK_KumaHeartbeat_KumaNotifications_KumaNotificationId",
                        column: x => x.KumaNotificationId,
                        principalTable: "KumaNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KumaMonitor",
                columns: table => new
                {
                    KumaNotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Url = table.Column<string>(type: "varchar(100)", nullable: false),
                    Hostname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Port = table.Column<string>(type: "varchar(100)", nullable: false),
                    Maxretries = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Keyword = table.Column<string>(type: "varchar(100)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KumaMonitor", x => x.KumaNotificationId);
                    table.ForeignKey(
                        name: "FK_KumaMonitor_KumaNotifications_KumaNotificationId",
                        column: x => x.KumaNotificationId,
                        principalTable: "KumaNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KumaHeartbeat");

            migrationBuilder.DropTable(
                name: "KumaMonitor");

            migrationBuilder.DropTable(
                name: "KumaNotifications");
        }
    }
}
