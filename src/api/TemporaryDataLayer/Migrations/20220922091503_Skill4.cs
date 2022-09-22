using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Skill4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Skills_Title",
                table: "Skills",
                newName: "IX_Title");

            migrationBuilder.AlterColumn<bool>(
                name: "ShowInAutoComplete",
                table: "Skills",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Title",
                table: "Skills",
                newName: "IX_Skills_Title");

            migrationBuilder.AlterColumn<bool>(
                name: "ShowInAutoComplete",
                table: "Skills",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
