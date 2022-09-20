using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.Wall_WallId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Wall_dbo.Organizations_OrganizationId",
                table: "Wall");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.Wall",
                table: "Wall");

            migrationBuilder.RenameTable(
                name: "Wall",
                newName: "Walls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.Walls",
                table: "Walls",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.Walls_WallId",
                table: "Events",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Walls_dbo.Organizations_OrganizationId",
                table: "Walls",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.Walls_WallId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Walls_dbo.Organizations_OrganizationId",
                table: "Walls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.Walls",
                table: "Walls");

            migrationBuilder.RenameTable(
                name: "Walls",
                newName: "Wall");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.Wall",
                table: "Wall",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.Wall_WallId",
                table: "Events",
                column: "WallId",
                principalTable: "Wall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Wall_dbo.Organizations_OrganizationId",
                table: "Wall",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
