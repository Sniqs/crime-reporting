using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawEnforcementApi.Migrations
{
    public partial class AddCrimeEventDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrimeEvent_Officers_OfficerId",
                table: "CrimeEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrimeEvent",
                table: "CrimeEvent");

            migrationBuilder.RenameTable(
                name: "CrimeEvent",
                newName: "CrimeEvents");

            migrationBuilder.RenameIndex(
                name: "IX_CrimeEvent_OfficerId",
                table: "CrimeEvents",
                newName: "IX_CrimeEvents_OfficerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficerId",
                table: "CrimeEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrimeEvents",
                table: "CrimeEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrimeEvents_Officers_OfficerId",
                table: "CrimeEvents",
                column: "OfficerId",
                principalTable: "Officers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrimeEvents_Officers_OfficerId",
                table: "CrimeEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrimeEvents",
                table: "CrimeEvents");

            migrationBuilder.RenameTable(
                name: "CrimeEvents",
                newName: "CrimeEvent");

            migrationBuilder.RenameIndex(
                name: "IX_CrimeEvents_OfficerId",
                table: "CrimeEvent",
                newName: "IX_CrimeEvent_OfficerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficerId",
                table: "CrimeEvent",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrimeEvent",
                table: "CrimeEvent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrimeEvent_Officers_OfficerId",
                table: "CrimeEvent",
                column: "OfficerId",
                principalTable: "Officers",
                principalColumn: "Id");
        }
    }
}
