using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class LotteryPartial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.LotteryParticipants_dbo.ApplicationUser_UserId",
                table: "LotteryParticipants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Joined",
                table: "LotteryParticipants",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.LotteryParticipants_dbo.AspNetUsers_UserId",
                table: "LotteryParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.LotteryParticipants_dbo.AspNetUsers_UserId",
                table: "LotteryParticipants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Joined",
                table: "LotteryParticipants",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.LotteryParticipants_dbo.ApplicationUser_UserId",
                table: "LotteryParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
