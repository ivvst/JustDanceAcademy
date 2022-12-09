using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustDanceAcademy.Data.Migrations
{
    public partial class SeedQuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelCategory",
                table: "Schedules");

            migrationBuilder.CreateTable(
                name: "FAQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQuestions", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "FAQuestions",
                columns: new[] { "Id", "Answear", "Question" },
                values: new object[] { 1, "No, after you start to train a class you will receive details about payment if you don't pay during 7 days you will receive second email and then you will not have the opportunity to start a train. ", "Can I start a class without payment?" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQuestions");

            migrationBuilder.AddColumn<string>(
                name: "LevelCategory",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

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
        }
    }
}
