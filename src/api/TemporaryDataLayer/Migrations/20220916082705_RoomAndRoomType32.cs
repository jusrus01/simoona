using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Floors_OfficeId",
                table: "Floors");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeId",
                table: "Floors",
                column: "OfficeId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OfficeId",
                table: "Floors");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_OfficeId",
                table: "Floors",
                column: "OfficeId");
        }
    }
}
