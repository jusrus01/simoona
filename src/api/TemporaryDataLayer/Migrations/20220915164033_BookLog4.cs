using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BookLog4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLog_Organizations_OrganizationId",
                table: "BookLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookLog",
                table: "BookLog");

            migrationBuilder.RenameTable(
                name: "BookLog",
                newName: "BookLogs");

            migrationBuilder.RenameIndex(
                name: "IX_BookLog_ApplicationUserId",
                table: "BookLogs",
                newName: "IX_BookLogs_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.BookLogs",
                table: "BookLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.Organizations_OrganizationId",
                table: "BookLogs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.Organizations_OrganizationId",
                table: "BookLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.BookLogs",
                table: "BookLogs");

            migrationBuilder.RenameTable(
                name: "BookLogs",
                newName: "BookLog");

            migrationBuilder.RenameIndex(
                name: "IX_BookLogs_ApplicationUserId",
                table: "BookLog",
                newName: "IX_BookLog_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookLog",
                table: "BookLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLog_Organizations_OrganizationId",
                table: "BookLog",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
