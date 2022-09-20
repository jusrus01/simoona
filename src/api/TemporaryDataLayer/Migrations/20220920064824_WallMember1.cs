using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallMember1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallsMembers_dbo.Walls_WallId",
                table: "WallsMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.WallsMembers",
                table: "WallsMembers");

            migrationBuilder.RenameTable(
                name: "WallsMembers",
                newName: "WallMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.WallMembers",
                table: "WallMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallMembers_dbo.Walls_WallId",
                table: "WallMembers",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WallMembers_dbo.Walls_WallId",
                table: "WallMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.WallMembers",
                table: "WallMembers");

            migrationBuilder.RenameTable(
                name: "WallMembers",
                newName: "WallsMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.WallsMembers",
                table: "WallsMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallsMembers_dbo.Walls_WallId",
                table: "WallsMembers",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
