using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustDanceAcademy.Data.Migrations
{
    public partial class SeedFinals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_AspNetUsers_StudentId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Classes_ClassId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Classes_StudentId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Plans",
                newName: "DetailTwo");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Schedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Schedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Plans",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DetailOne",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailThree",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "Instrustors",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassStudents_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassStudents_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40756c11-64d8-41b2-8069-d41a93753530", "AQAAAAEAACcQAAAAEF2Bl+ENJMnZz2G1pB9VUI/Mp/4Tevdb3hERUhUqdISCmMg/QENuA5uFpbSrEy5L2w==", "1ebca965-1f2d-48d8-bad7-865ba2301b8c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46ae1bf0-94ef-4d40-bb90-15a6a94438c3", "AQAAAAEAACcQAAAAEKq3ZshheJd2G1CZVIHbeLKcg3uEyn+g01Qo1l5CtxSOFPCVcL1wc8e68Ck9kyY0CA==", "0d381df7-9233-4853-a1ba-7dc8ffa2407e" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_IsDeleted",
                table: "Schedules",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudents_ClassId",
                table: "ClassStudents",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudents_IsDeleted",
                table: "ClassStudents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudents_StudentId",
                table: "ClassStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClassId",
                table: "Reviews",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IsDeleted",
                table: "Reviews",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Classes_ClassId",
                table: "Schedules",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Classes_ClassId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "ClassStudents");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_IsDeleted",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DetailOne",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DetailThree",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DetailTwo",
                table: "Plans",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Schedules",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Plans",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "Instrustors",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(700)",
                oldMaxLength: 700);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Classes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4d6890d-1450-444c-a627-b5fd7cbf0765", "AQAAAAEAACcQAAAAEIZ8+Hd7LG4Z2s6Sen662yA/p1Br607LmVtboaNGR48cTjEo7apfdf4FxdBnAitmFA==", "5e878f28-fba2-4f13-be2a-e82c06e75a84" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfc80c3c-f268-416d-9411-332c312043ef", "AQAAAAEAACcQAAAAEIZuIRQvfbT+TpwdEteZUh4sMVD6BDjQKwmTSuIyOLCPpejFzm0ydi0Ya9HJdtjnzw==", "42dac9a1-47b4-4f1e-8239-852a0e692c5f" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_StudentId",
                table: "Classes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_AspNetUsers_StudentId",
                table: "Classes",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Classes_ClassId",
                table: "Schedules",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
