using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureId",
                table: "Floors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureId1",
                table: "Floors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_PictureId1",
                table: "Floors",
                column: "PictureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Floors_dbo.Pictures_PictureId1",
                table: "Floors",
                column: "PictureId1",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Floors_dbo.Pictures_PictureId1",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_PictureId1",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "PictureId1",
                table: "Floors");
        }
    }
}
