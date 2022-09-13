using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModuleOrganizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOrganizations_Module_Id",
                table: "ModuleOrganizations",
                column: "Module_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOrganizations_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModuleOrganizations_Module_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_ModuleOrganizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOrganizations_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id");
        }
    }
}
