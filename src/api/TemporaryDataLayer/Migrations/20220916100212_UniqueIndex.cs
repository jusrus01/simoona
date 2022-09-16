using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class UniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookId",
                table: "BookOffices");

            migrationBuilder.CreateIndex(
                name: "IX_BookOffices_BookId_OfficeId",
                table: "BookOffices",
                columns: new[] { "BookId", "OfficeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookOffices_BookId_OfficeId",
                table: "BookOffices");

            migrationBuilder.CreateIndex(
                name: "IX_BookId",
                table: "BookOffices",
                column: "BookId");
        }
    }
}
