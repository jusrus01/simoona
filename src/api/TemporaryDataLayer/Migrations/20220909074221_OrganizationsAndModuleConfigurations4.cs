using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class OrganizationsAndModuleConfigurations4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleOrganization_Modules_ModuleId",
                table: "ModuleOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleOrganization_Organizations_OrganizationId",
                table: "ModuleOrganization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleOrganization",
                table: "ModuleOrganization");

            migrationBuilder.RenameTable(
                name: "ModuleOrganization",
                newName: "ModuleOrganizations");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "ModuleOrganizations",
                newName: "Organization_Id");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "ModuleOrganizations",
                newName: "Module_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleOrganization_OrganizationId",
                table: "ModuleOrganizations",
                newName: "IX_ModuleOrganizations_Organization_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleOrganizations",
                table: "ModuleOrganizations",
                columns: new[] { "Module_Id", "Organization_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleOrganizations_Modules_Module_Id",
                table: "ModuleOrganizations",
                column: "Module_Id",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleOrganizations_Organizations_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleOrganizations_Modules_Module_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleOrganizations_Organizations_Organization_Id",
                table: "ModuleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleOrganizations",
                table: "ModuleOrganizations");

            migrationBuilder.RenameTable(
                name: "ModuleOrganizations",
                newName: "ModuleOrganization");

            migrationBuilder.RenameColumn(
                name: "Organization_Id",
                table: "ModuleOrganization",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "Module_Id",
                table: "ModuleOrganization",
                newName: "ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleOrganizations_Organization_Id",
                table: "ModuleOrganization",
                newName: "IX_ModuleOrganization_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleOrganization",
                table: "ModuleOrganization",
                columns: new[] { "ModuleId", "OrganizationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleOrganization_Modules_ModuleId",
                table: "ModuleOrganization",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleOrganization_Organizations_OrganizationId",
                table: "ModuleOrganization",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
