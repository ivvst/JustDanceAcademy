using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustDanceAcademy.Data.Migrations
{
    public partial class SeedNewPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "TestStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestStudents_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestStudents_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf6b0a7f-0720-49df-a866-92efd216277b", "AQAAAAEAACcQAAAAEJ3m7llHwqyNIrxZD5Fa6Yim9W6a2G4o//xwrMQsBSBZYK58SU33tRl77nR+fLY++Q==", "40a1bd9b-dc83-4863-bee5-bd2d5d87012b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30d64e6e-c770-455c-931a-b44e443ae942", "AQAAAAEAACcQAAAAEL5DR9Q0v8L+gfsOeeKNde9ygFHSOxwW4RxjKUjc/bdZARXUsfK11Zc1zpdFREpq+Q==", "59575370-f787-4859-91bb-cd7ad0155dd3" });

            migrationBuilder.CreateIndex(
                name: "IX_TestStudents_IsDeleted",
                table: "TestStudents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TestStudents_PlanId",
                table: "TestStudents",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStudents_StudentId",
                table: "TestStudents",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestStudents");

            migrationBuilder.CreateTable(
                name: "PlanStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanStudents_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanStudents_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5ce2f4a-22f8-4ddc-854e-bcdec82bd1bb", "AQAAAAEAACcQAAAAEGie0GKrvS7RAZU0P2Q6ekWfNNtvEggJdW0WZAMwfR+pS0PFNF23ORzCo+0JZAoTEQ==", "df382c2c-cf75-4418-8489-7b31099c0049" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f9b8272-a59b-44eb-81f2-6a955dbd41be", "AQAAAAEAACcQAAAAENJ4BctDTwRFx3Qs5tLs/euBy/iw4Z1EzmzoIp40h+iZDhdd8ICjy2j39sIPEyIXCQ==", "456b9504-c2f4-4757-b681-d86029f6af90" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanStudents_IsDeleted",
                table: "PlanStudents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlanStudents_PlanId",
                table: "PlanStudents",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanStudents_StudentId",
                table: "PlanStudents",
                column: "StudentId");
        }
    }
}
