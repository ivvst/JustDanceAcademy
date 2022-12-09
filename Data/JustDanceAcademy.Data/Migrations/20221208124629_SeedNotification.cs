using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustDanceAcademy.Data.Migrations
{
    public partial class SeedNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "d67d9f74-c9a6-463c-84e2-1888a8149d58", "AQAAAAEAACcQAAAAEIDpaY47hDzlHr05mXXPApGsXGsyMe0dWiWQCVUoFqhM2PUKPGHPuljVYJL7GzGGNg==", "a94d8484-770d-4995-aa91-e1426c3b3a5b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e219f78f-8dc1-4a5d-81b3-dfc6b2d39e00", "AQAAAAEAACcQAAAAEOWfTgAlePPLqmasjyW1SruD3jI+GcBmwDUXXkQbzRtjd0scwxrFfCZ1CDil6f5Kpw==", "6e804817-486a-4ea9-8ccd-aa5267428ea2" });
        }
    }
}
