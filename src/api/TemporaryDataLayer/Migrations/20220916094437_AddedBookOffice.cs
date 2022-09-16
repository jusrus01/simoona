using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class AddedBookOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs");

            migrationBuilder.AlterColumn<int>(
                name: "BookOfficeId",
                table: "BookLogs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs");

            migrationBuilder.AlterColumn<int>(
                name: "BookOfficeId",
                table: "BookLogs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
