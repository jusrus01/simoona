using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ChangedExternalOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExternalLinks_OrganizationId",
                table: "ExternalLinks");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "ExternalLinks",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "ExternalLinks");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_OrganizationId",
                table: "ExternalLinks",
                column: "OrganizationId");
        }
    }
}
