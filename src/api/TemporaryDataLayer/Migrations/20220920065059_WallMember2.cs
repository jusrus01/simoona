using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallMember2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "hhh",
                table: "WallMembers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WallMembers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "EmailNotificationsEnabled",
                table: "WallMembers",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "AppNotificationsEnabled",
                table: "WallMembers",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallUsers_dbo.AspNetUsers_UserId",
                table: "WallMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallUsers_dbo.AspNetUsers_UserId",
                table: "WallMembers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WallMembers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EmailNotificationsEnabled",
                table: "WallMembers",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AppNotificationsEnabled",
                table: "WallMembers",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "hhh",
                table: "WallMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
