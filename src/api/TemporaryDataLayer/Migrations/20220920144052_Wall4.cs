using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Wall4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.ApplicationUser_UserId",
                table: "WallModerators");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.AspNetUsers_UserId",
                table: "WallModerators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.AspNetUsers_UserId",
                table: "WallModerators");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.ApplicationUser_UserId",
                table: "WallModerators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
