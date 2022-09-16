using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BookLog3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLog_AspNetUsers_ApplicationUserId",
                table: "BookLog");

            migrationBuilder.AddForeignKey(
                name: "IX_ApplicationUser",
                table: "BookLog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "IX_ApplicationUser",
                table: "BookLog");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLog_AspNetUsers_ApplicationUserId",
                table: "BookLog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
