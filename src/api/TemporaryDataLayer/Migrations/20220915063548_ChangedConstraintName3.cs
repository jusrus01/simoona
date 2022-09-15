using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ChangedConstraintName3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleOrganizations",
                table: "ModuleOrganizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.ModuleOrganizations",
                table: "ModuleOrganizations",
                columns: new[] { "Module_Id", "Organization_Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.ModuleOrganizations",
                table: "ModuleOrganizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleOrganizations",
                table: "ModuleOrganizations",
                columns: new[] { "Module_Id", "Organization_Id" });
        }
    }
}
