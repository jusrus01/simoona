using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class KudosLog4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosLogs_dbo.Organizations_OrganizationId",
                table: "KudosLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosLogs_dbo.Organizations_OrganizationId",
                table: "KudosLogs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosLogs_dbo.Organizations_OrganizationId",
                table: "KudosLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosLogs_dbo.Organizations_OrganizationId",
                table: "KudosLogs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
