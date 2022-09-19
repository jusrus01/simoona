using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class LotteryPartial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                table: "Lotteries");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRefundFailed",
                table: "Lotteries",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "GiftedTicketLimit",
                table: "Lotteries",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Lotteries",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                table: "Lotteries",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                table: "Lotteries");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRefundFailed",
                table: "Lotteries",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "GiftedTicketLimit",
                table: "Lotteries",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Lotteries",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                table: "Lotteries",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
