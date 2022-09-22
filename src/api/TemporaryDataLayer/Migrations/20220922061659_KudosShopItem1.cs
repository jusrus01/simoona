using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class KudosShopItem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosShopItems_dbo.Organizations_OrganizationId",
                table: "KudosShopItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "KudosShopItems",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "KudosShopItems",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KudosShopItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosShopItems_dbo.Organizations_OrganizationId",
                table: "KudosShopItems",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosShopItems_dbo.Organizations_OrganizationId",
                table: "KudosShopItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KudosShopItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "KudosShopItems",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "KudosShopItems",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosShopItems_dbo.Organizations_OrganizationId",
                table: "KudosShopItems",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
