using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Floors_PictureId",
                table: "Floors");

            migrationBuilder.RenameColumn(
                name: "PictureId1",
                table: "Floors",
                newName: "Picture_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Floors_PictureId1",
                table: "Floors",
                newName: "IX_Floors_Picture_Id");

            migrationBuilder.AlterColumn<string>(
                name: "PictureId",
                table: "Floors",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture_Id",
                table: "Floors",
                newName: "PictureId1");

            migrationBuilder.RenameIndex(
                name: "IX_Floors_Picture_Id",
                table: "Floors",
                newName: "IX_Floors_PictureId1");

            migrationBuilder.AlterColumn<string>(
                name: "PictureId",
                table: "Floors",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_PictureId",
                table: "Floors",
                column: "PictureId")
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
