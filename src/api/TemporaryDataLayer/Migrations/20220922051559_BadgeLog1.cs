using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BadgeLog1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeLogs_dbo.ApplicationUser_EmployeeId",
                table: "BadgeLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "BadgeLogs",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "BadgeLogs",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeLogs_dbo.AspNetUsers_EmployeeId",
                table: "BadgeLogs",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeLogs_dbo.AspNetUsers_EmployeeId",
                table: "BadgeLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "BadgeLogs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "BadgeLogs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeLogs_dbo.ApplicationUser_EmployeeId",
                table: "BadgeLogs",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
