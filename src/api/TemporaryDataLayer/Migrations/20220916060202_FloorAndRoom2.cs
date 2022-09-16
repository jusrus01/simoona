using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class FloorAndRoom2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floors_FloorId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FloorId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FloorId1",
                table: "Rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorId1",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId1",
                table: "Rooms",
                column: "FloorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floors_FloorId1",
                table: "Rooms",
                column: "FloorId1",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
