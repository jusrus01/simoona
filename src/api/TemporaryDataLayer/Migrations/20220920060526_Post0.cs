using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Post0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "Posts",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Posts",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivity",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdit",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MessageBody",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SharedEventId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WallId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WallId",
                table: "Posts",
                column: "WallId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Posts_dbo.ApplicationUser_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Posts_dbo.Walls_WallId",
                table: "Posts",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Posts_dbo.ApplicationUser_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Posts_dbo.Walls_WallId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_AuthorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_WallId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastEdit",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "MessageBody",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SharedEventId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
