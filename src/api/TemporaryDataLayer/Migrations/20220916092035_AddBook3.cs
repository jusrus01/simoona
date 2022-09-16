using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class AddBook3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Books_ApplicationUserId",
                table: "Books",
                newName: "IX_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers",
                newName: "IX_RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserId",
                table: "Books",
                newName: "IX_Books_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RoomId");
        }
    }
}
