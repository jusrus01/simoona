using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class AddedBookOffice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookOffices_dbo.Offices_OfficeId",
                table: "BookOffices");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookOffices_dbo.Organizations_OrganizationId",
                table: "BookOffices");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "BookOffices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "BookOffices",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "BookOffices",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "BookOffices",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookOffices_dbo.Offices_OfficeId",
                table: "BookOffices",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookOffices_dbo.Organizations_OrganizationId",
                table: "BookOffices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookOffices_dbo.Offices_OfficeId",
                table: "BookOffices");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookOffices_dbo.Organizations_OrganizationId",
                table: "BookOffices");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "BookOffices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "BookOffices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "BookOffices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "BookOffices",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookOffices_dbo.Offices_OfficeId",
                table: "BookOffices",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookOffices_dbo.Organizations_OrganizationId",
                table: "BookOffices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
