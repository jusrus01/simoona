using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Wall2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.AspNetUsers_UserId",
                table: "WallModerators");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                table: "WallModerators");

            migrationBuilder.DropForeignKey(
                name: "FK_WallModerators_Walls_WallId1",
                table: "WallModerators");

            migrationBuilder.DropIndex(
                name: "IX_WallModerators_WallId1",
                table: "WallModerators");

            migrationBuilder.DropColumn(
                name: "WallId1",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.ApplicationUser_UserId",
                table: "WallModerators");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                table: "WallModerators");

            migrationBuilder.AddColumn<int>(
                name: "WallId1",
                table: "WallModerators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WallModerators_WallId1",
                table: "WallModerators",
                column: "WallId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WallModerators_Walls_WallId1",
                table: "WallModerators",
                column: "WallId1",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
