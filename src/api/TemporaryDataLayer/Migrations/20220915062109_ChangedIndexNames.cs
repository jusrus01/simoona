using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ChangedIndexNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_ModuleOrganizations_Organization_Id",
                table: "ModuleOrganizations",
                newName: "IX_Organization_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleOrganizations_Module_Id",
                table: "ModuleOrganizations",
                newName: "IX_Module_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Organization_Id",
                table: "ModuleOrganizations",
                newName: "IX_ModuleOrganizations_Organization_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Module_Id",
                table: "ModuleOrganizations",
                newName: "IX_ModuleOrganizations_Module_Id");
        }
    }
}
