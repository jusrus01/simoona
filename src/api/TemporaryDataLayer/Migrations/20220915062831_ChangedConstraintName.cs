using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ChangedConstraintName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ModuleOrganizations_dbo.Modules_Module_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ModuleOrganizations_dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.dbo.ShroomsModuleOrganizations_dbo.dbo.ShroomsModules_ShroomsModule_Id",
                table: "ModuleOrganizations",
                column: "Module_Id",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.dbo.ShroomsModuleOrganizations_dbo.dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.dbo.ShroomsModuleOrganizations_dbo.dbo.ShroomsModules_ShroomsModule_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.dbo.ShroomsModuleOrganizations_dbo.dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ModuleOrganizations_dbo.Modules_Module_Id",
                table: "ModuleOrganizations",
                column: "Module_Id",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ModuleOrganizations_dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
