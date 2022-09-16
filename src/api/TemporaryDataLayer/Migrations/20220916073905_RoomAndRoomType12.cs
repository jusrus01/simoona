using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RoomTypes",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RoomTypes",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
