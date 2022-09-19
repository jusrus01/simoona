using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class AddedBlacklistUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_ModifiedBy",
                table: "BlacklistUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_UserId",
                table: "BlacklistUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_ModifiedBy",
                table: "BlacklistUsers",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_UserId",
                table: "BlacklistUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_ModifiedBy",
                table: "BlacklistUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_UserId",
                table: "BlacklistUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_ModifiedBy",
                table: "BlacklistUsers",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_UserId",
                table: "BlacklistUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
