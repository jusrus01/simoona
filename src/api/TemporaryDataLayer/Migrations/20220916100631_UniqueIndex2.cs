using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class UniqueIndex2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_BookOffices_BookId_OfficeId",
                table: "BookOffices",
                newName: "BookId_OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "BookId_OfficeId",
                table: "BookOffices",
                newName: "IX_BookOffices_BookId_OfficeId");
        }
    }
}
