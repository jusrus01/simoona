using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLink_Organizations_OrganizationId",
                table: "ExternalLink");

            migrationBuilder.DropForeignKey(
                name: "FK_filterPresets_Organizations_OrganizationId",
                table: "filterPresets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_filterPresets",
                table: "filterPresets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalLink",
                table: "ExternalLink");

            migrationBuilder.RenameTable(
                name: "filterPresets",
                newName: "FilterPresets");

            migrationBuilder.RenameTable(
                name: "ExternalLink",
                newName: "ExternalLinks");

            migrationBuilder.RenameIndex(
                name: "IX_filterPresets_OrganizationId",
                table: "FilterPresets",
                newName: "IX_FilterPresets_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalLink_OrganizationId",
                table: "ExternalLinks",
                newName: "IX_ExternalLinks_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilterPresets",
                table: "FilterPresets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalLinks",
                table: "ExternalLinks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLinks_Organizations_OrganizationId",
                table: "ExternalLinks",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilterPresets_Organizations_OrganizationId",
                table: "FilterPresets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLinks_Organizations_OrganizationId",
                table: "ExternalLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterPresets_Organizations_OrganizationId",
                table: "FilterPresets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilterPresets",
                table: "FilterPresets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalLinks",
                table: "ExternalLinks");

            migrationBuilder.RenameTable(
                name: "FilterPresets",
                newName: "filterPresets");

            migrationBuilder.RenameTable(
                name: "ExternalLinks",
                newName: "ExternalLink");

            migrationBuilder.RenameIndex(
                name: "IX_FilterPresets_OrganizationId",
                table: "filterPresets",
                newName: "IX_filterPresets_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalLinks_OrganizationId",
                table: "ExternalLink",
                newName: "IX_ExternalLink_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_filterPresets",
                table: "filterPresets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalLink",
                table: "ExternalLink",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLink_Organizations_OrganizationId",
                table: "ExternalLink",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_filterPresets_Organizations_OrganizationId",
                table: "filterPresets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
