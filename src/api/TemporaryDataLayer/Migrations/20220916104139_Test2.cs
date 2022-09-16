using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLogs_BookOffices_BookOfficeId1",
                table: "BookLogs");

            migrationBuilder.DropIndex(
                name: "IX_BookLogs_BookOfficeId1",
                table: "BookLogs");

            migrationBuilder.DropColumn(
                name: "BookOfficeId1",
                table: "BookLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookOfficeId1",
                table: "BookLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLogs_BookOfficeId1",
                table: "BookLogs",
                column: "BookOfficeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLogs_BookOffices_BookOfficeId1",
                table: "BookLogs",
                column: "BookOfficeId1",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
