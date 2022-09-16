using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ApplicationUser_dbo.Rooms_RoomId",
                table: "AspNetUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ApplicationUser_dbo.Rooms_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
