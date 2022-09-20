using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wall_Organizations_OrganizationId",
                table: "Wall");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wall",
                table: "Wall");

            migrationBuilder.AddColumn<int>(
                name: "WallId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.Wall",
                table: "Wall",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WallId",
                table: "Events",
                column: "WallId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_WallId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "Events");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wall",
                table: "Wall",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wall_Organizations_OrganizationId",
                table: "Wall",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
