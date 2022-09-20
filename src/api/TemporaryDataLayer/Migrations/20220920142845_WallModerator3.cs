using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallModerator3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.ApplicationUser_UserId",
                table: "WallModerators");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                table: "WallModerators");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.AspNetUsers_UserId",
                table: "WallModerators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                table: "WallModerators",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.AspNetUsers_UserId",
                table: "WallModerators");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                table: "WallModerators");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.ApplicationUser_UserId",
                table: "WallModerators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                table: "WallModerators",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
