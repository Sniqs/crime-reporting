using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawEnforcementApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallSign = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OfficerRankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Officers_Ranks_OfficerRankId",
                        column: x => x.OfficerRankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrimeEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrimeEventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrimeEvent_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrimeEvent_OfficerId",
                table: "CrimeEvent",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Officers_CallSign",
                table: "Officers",
                column: "CallSign",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Officers_OfficerRankId",
                table: "Officers",
                column: "OfficerRankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrimeEvent");

            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "Ranks");
        }
    }
}
