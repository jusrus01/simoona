using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Floors_dbo.Pictures_PictureId1",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_Picture_Id",
                table: "Floors");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_Id",
                table: "Floors",
                column: "Picture_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Floors_dbo.Pictures_Picture_Id",
                table: "Floors",
                column: "Picture_Id",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Floors_dbo.Pictures_Picture_Id",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Picture_Id",
                table: "Floors");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_Picture_Id",
                table: "Floors",
                column: "Picture_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Floors_dbo.Pictures_PictureId1",
                table: "Floors",
                column: "Picture_Id",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
