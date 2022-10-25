using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawEnforcementApi.Migrations
{
    public partial class AddSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CrimeEventId",
                table: "CrimeEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("1685cae0-71e4-4e76-83a9-5c9488b3942f"), "Lieutenant" });

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("8960db50-77bd-4ea1-9038-952eb09a4eb5"), "Sergeant" });

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ebb26b52-b7b9-42a2-8687-b06d1b2011ee"), "Deputy" });

            migrationBuilder.InsertData(
                table: "Officers",
                columns: new[] { "Id", "CallSign", "OfficerRankId" },
                values: new object[] { new Guid("01fde555-3de6-4ad2-ab32-ac725a6391e9"), "Tango-4", new Guid("8960db50-77bd-4ea1-9038-952eb09a4eb5") });

            migrationBuilder.InsertData(
                table: "Officers",
                columns: new[] { "Id", "CallSign", "OfficerRankId" },
                values: new object[] { new Guid("098603a4-895a-4b1e-85ee-bb83ed801c0a"), "Echo-1", new Guid("1685cae0-71e4-4e76-83a9-5c9488b3942f") });

            migrationBuilder.InsertData(
                table: "Officers",
                columns: new[] { "Id", "CallSign", "OfficerRankId" },
                values: new object[] { new Guid("b37e6518-d42b-4809-b848-c1501cf5af80"), "Charlie-10", new Guid("ebb26b52-b7b9-42a2-8687-b06d1b2011ee") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Officers",
                keyColumn: "Id",
                keyValue: new Guid("01fde555-3de6-4ad2-ab32-ac725a6391e9"));

            migrationBuilder.DeleteData(
                table: "Officers",
                keyColumn: "Id",
                keyValue: new Guid("098603a4-895a-4b1e-85ee-bb83ed801c0a"));

            migrationBuilder.DeleteData(
                table: "Officers",
                keyColumn: "Id",
                keyValue: new Guid("b37e6518-d42b-4809-b848-c1501cf5af80"));

            migrationBuilder.DeleteData(
                table: "Ranks",
                keyColumn: "Id",
                keyValue: new Guid("1685cae0-71e4-4e76-83a9-5c9488b3942f"));

            migrationBuilder.DeleteData(
                table: "Ranks",
                keyColumn: "Id",
                keyValue: new Guid("8960db50-77bd-4ea1-9038-952eb09a4eb5"));

            migrationBuilder.DeleteData(
                table: "Ranks",
                keyColumn: "Id",
                keyValue: new Guid("ebb26b52-b7b9-42a2-8687-b06d1b2011ee"));

            migrationBuilder.AlterColumn<string>(
                name: "CrimeEventId",
                table: "CrimeEvents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
