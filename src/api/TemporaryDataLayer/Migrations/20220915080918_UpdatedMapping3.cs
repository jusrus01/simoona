using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class UpdatedMapping3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AspNetUsers_ApplicationUserId",
                table: "WorkingHours");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "WorkingHours",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_AspNetUsers_ApplicationUserId",
                table: "WorkingHours",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AspNetUsers_ApplicationUserId",
                table: "WorkingHours");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "WorkingHours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_AspNetUsers_ApplicationUserId",
                table: "WorkingHours",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
