using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Project4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Projects_dbo.Organizations_OrganizationId",
                table: "Projects");

            migrationBuilder.RenameIndex(
                name: "FK_Org_Projects",
                table: "Projects",
                newName: "IX_OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Org_Projects",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Org_Projects",
                table: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationId",
                table: "Projects",
                newName: "FK_Org_Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Projects_dbo.Organizations_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
