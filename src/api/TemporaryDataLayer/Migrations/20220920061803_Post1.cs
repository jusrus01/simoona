using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Post1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Posts_dbo.ApplicationUser_AuthorId",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "EventParticipants",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LastActivity",
                table: "Posts",
                column: "LastActivity")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Posts_dbo.AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Posts_dbo.AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_LastActivity",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "EventParticipants",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldDefaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Posts_dbo.ApplicationUser_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
