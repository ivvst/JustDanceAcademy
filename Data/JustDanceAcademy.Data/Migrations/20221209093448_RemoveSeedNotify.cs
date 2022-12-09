using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustDanceAcademy.Data.Migrations
{
    public partial class RemoveSeedNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HubConnections");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ad98ae3-eb06-44da-a04d-58ae142c5916", "AQAAAAEAACcQAAAAEGvjQH+uZDq0msqMgKf+p8sEeia4RBEtrSeKonU+nIlVZTTItmTdFP2tT0hLfaHH7w==", "265241ce-dd8c-47b1-8648-749e3c243f66" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "093e2101-8dc2-4e5c-96ff-0e16a1fa8fa6", "AQAAAAEAACcQAAAAEM3/NwYmSYG2qcaU475Evoeit2pckhCT/hJ9FCdBQ5PMLjirpmwPjCi96My6O8LRDA==", "7c879ecf-5762-4b92-aa5f-b6b27f3b7a53" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HubConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b406465c-10eb-4d39-8263-a4af426c6d44", "AQAAAAEAACcQAAAAEBFAR4v/3lZ1guDAgjTXpC3ciDwgxbZoXMalpNFdunH8xDDZbMDH42XG8VVC5UyKLQ==", "4833c2d6-5428-4623-9753-a8c214b23559" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0b6b951-36b6-4b82-93dd-b04248cbf527", "AQAAAAEAACcQAAAAEHLNv/44K+uj7QT+l2e6WkIXWNg2H0bKi3sV3rRS29O1GwPsJ0hnxMtivT2u/jpacw==", "e576a88e-3484-4f8f-b31c-c8b5e6c0f65c" });
        }
    }
}
