using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class CommitteeSuggestion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.CommitteeSuggestions_dbo.ApplicationUser_UserId",
                table: "CommitteeSuggestions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CommitteeSuggestions",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.CommitteeSuggestions_dbo.AspNetUsers_UserId",
                table: "CommitteeSuggestions",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.CommitteeSuggestions_dbo.AspNetUsers_UserId",
                table: "CommitteeSuggestions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CommitteeSuggestions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.CommitteeSuggestions_dbo.ApplicationUser_UserId",
                table: "CommitteeSuggestions",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
