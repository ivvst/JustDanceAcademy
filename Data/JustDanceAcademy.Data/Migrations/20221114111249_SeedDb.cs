using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustDanceAcademy.Data.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelsCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelCategoryId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_LevelsCategory_LevelCategoryId",
                        column: x => x.LevelCategoryId,
                        principalTable: "LevelsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instrustors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrustors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instrustors_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "200adb3d-b3f4-4bde-a9c8-2c6888d6be30", 0, "e4d6890d-1450-444c-a627-b5fd7cbf0765", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "guest@mail.com", false, false, false, null, null, "GUEST@MAIL.com", "GUEST", "AQAAAAEAACcQAAAAEIZ8+Hd7LG4Z2s6Sen662yA/p1Br607LmVtboaNGR48cTjEo7apfdf4FxdBnAitmFA==", null, false, "5e878f28-fba2-4f13-be2a-e82c06e75a84", false, "guest" },
                    { "8fe346ea-30ce-4b6e-b67a-fedc225845c1", 0, "cfc80c3c-f268-416d-9411-332c312043ef", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vanis@mail.com", false, false, false, null, null, "VANIS@MAIL.com", "VANIS", "AQAAAAEAACcQAAAAEIZuIRQvfbT+TpwdEteZUh4sMVD6BDjQKwmTSuIyOLCPpejFzm0ydi0Ya9HJdtjnzw==", null, false, "42dac9a1-47b4-4f1e-8239-852a0e692c5f", false, "vanis" }
                });

            migrationBuilder.InsertData(
                table: "LevelsCategory",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "Getting Started" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "Begginer" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "InterMediate" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "Advanced" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "Kids" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "ImageUrl", "Instructor", "IsDeleted", "LevelCategoryId", "ModifiedOn", "Name", "StudentId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Keep your head up,with your feet on the ground", "https://cf.ltkcdn.net/dance/images/std/224320-630x450-hip-hop.jpg", "Aaliyah", false, 2, null, "Hip Hop", null });

            migrationBuilder.InsertData(
                table: "Instrustors",
                columns: new[] { "Id", "Biography", "ClassId", "CreatedOn", "DeletedOn", "ImageUrl", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[] { 1, "Aaliyah Dana Haughton was an American singer. She has been credited for helping to redefine contemporary R&B, pop and hip hop, earning her the nicknames the -Princess of R&B- and -Queen of Urban Pop-", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "https://th.bing.com/th/id/OIP.qe-SelqFhGlvOpoOrNKO-AHaJr?pid=ImgDet&rs=1", false, null, "Aaliyah" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IsDeleted",
                table: "Classes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LevelCategoryId",
                table: "Classes",
                column: "LevelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_StudentId",
                table: "Classes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrustors_ClassId",
                table: "Instrustors",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrustors_IsDeleted",
                table: "Instrustors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LevelsCategory_IsDeleted",
                table: "LevelsCategory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_IsDeleted",
                table: "Plans",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassId",
                table: "Schedules",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instrustors");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "LevelsCategory");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200adb3d-b3f4-4bde-a9c8-2c6888d6be30");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe346ea-30ce-4b6e-b67a-fedc225845c1");
        }
    }
}
