using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture_Id",
                table: "Floors",
                newName: "PictureId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Floors_PictureId",
                table: "Floors");

            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Floors",
                newName: "Picture_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Picture_Id",
                table: "Floors",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
