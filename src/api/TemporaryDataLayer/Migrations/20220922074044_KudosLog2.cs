using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class KudosLog2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosLogs_dbo.ApplicationUser_EmployeeId",
                table: "KudosLogs");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeId",
                table: "KudosLogs");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "KudosLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employee_Id",
                table: "KudosLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KudosLogs_Employee_Id",
                table: "KudosLogs",
                column: "Employee_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosLogs_dbo.AspNetUsers_Employee_Id",
                table: "KudosLogs",
                column: "Employee_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosLogs_dbo.AspNetUsers_Employee_Id",
                table: "KudosLogs");

            migrationBuilder.DropIndex(
                name: "IX_KudosLogs_Employee_Id",
                table: "KudosLogs");

            migrationBuilder.DropColumn(
                name: "Employee_Id",
                table: "KudosLogs");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "KudosLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeId",
                table: "KudosLogs",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosLogs_dbo.ApplicationUser_EmployeeId",
                table: "KudosLogs",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
