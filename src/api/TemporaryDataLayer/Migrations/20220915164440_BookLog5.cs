using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BookLog5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "IX_ApplicationUser",
                table: "BookLogs");

            migrationBuilder.DropIndex(
                name: "IX_BookLogs_ApplicationUserId",
                table: "BookLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TakenFrom",
                table: "BookLogs",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Returned",
                table: "BookLogs",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserId",
                table: "BookLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TakenFrom",
                table: "BookLogs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Returned",
                table: "BookLogs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLogs_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "IX_ApplicationUser",
                table: "BookLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
