using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Wall1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Access",
                table: "Walls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AddForNewUsers",
                table: "Walls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Walls",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHiddenFromAllWalls",
                table: "Walls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Walls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Walls",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Walls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WallId1",
                table: "WallModerators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WallModerators_WallId1",
                table: "WallModerators",
                column: "WallId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WallModerators_Walls_WallId1",
                table: "WallModerators",
                column: "WallId1",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WallModerators_Walls_WallId1",
                table: "WallModerators");

            migrationBuilder.DropIndex(
                name: "IX_WallModerators_WallId1",
                table: "WallModerators");

            migrationBuilder.DropColumn(
                name: "Access",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "AddForNewUsers",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "IsHiddenFromAllWalls",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "WallId1",
                table: "WallModerators");
        }
    }
}
