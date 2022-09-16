using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class AddedBookOffice2 : Migration
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
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BookOfficeId1",
                table: "BookLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLogs_BookOfficeId1",
                table: "BookLogs",
                column: "BookOfficeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLogs_BookOffices_BookOfficeId1",
                table: "BookLogs",
                column: "BookOfficeId1",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLogs_BookOffices_BookOfficeId1",
                table: "BookLogs");

            migrationBuilder.DropIndex(
                name: "IX_BookLogs_BookOfficeId1",
                table: "BookLogs");

            migrationBuilder.DropColumn(
                name: "BookOfficeId1",
                table: "BookLogs");

            migrationBuilder.AlterColumn<int>(
                name: "BookOfficeId",
                table: "BookLogs",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
