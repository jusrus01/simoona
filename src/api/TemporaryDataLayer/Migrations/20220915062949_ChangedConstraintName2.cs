using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ChangedConstraintName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.dbo.ShroomsModuleOrganizations_dbo.dbo.ShroomsModules_ShroomsModule_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.dbo.ShroomsModuleOrganizations_dbo.dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.ModuleOrganizations",
                table: "ModuleOrganizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleOrganizations",
                table: "ModuleOrganizations",
                columns: new[] { "Module_Id", "Organization_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ShroomsModuleOrganizations_dbo.ShroomsModules_ShroomsModule_Id",
                table: "ModuleOrganizations",
                column: "Module_Id",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ShroomsModuleOrganizations_dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ShroomsModuleOrganizations_dbo.ShroomsModules_ShroomsModule_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ShroomsModuleOrganizations_dbo.Organizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleOrganizations",
                table: "ModuleOrganizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.ModuleOrganizations",
                table: "ModuleOrganizations",
                columns: new[] { "Module_Id", "Organization_Id" });

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
    }
}
