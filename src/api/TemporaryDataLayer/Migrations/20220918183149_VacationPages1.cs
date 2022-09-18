using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class VacationPages1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.VacationPages_dbo.Organizations_OrganizationId",
                table: "VacationPages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "VacationPages",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "VacationPages",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.VacationPages_dbo.Organizations_OrganizationId",
                table: "VacationPages",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.VacationPages_dbo.Organizations_OrganizationId",
                table: "VacationPages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "VacationPages",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "VacationPages",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.VacationPages_dbo.Organizations_OrganizationId",
                table: "VacationPages",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
