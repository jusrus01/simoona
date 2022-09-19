using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class LotteryPartial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lottery_Organizations_OrganizationId",
                table: "Lottery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lottery",
                table: "Lottery");

            migrationBuilder.RenameTable(
                name: "Lottery",
                newName: "Lotteries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.Lotteries",
                table: "Lotteries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                table: "Lotteries",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                table: "Lotteries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.Lotteries",
                table: "Lotteries");

            migrationBuilder.RenameTable(
                name: "Lotteries",
                newName: "Lottery");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lottery",
                table: "Lottery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lottery_Organizations_OrganizationId",
                table: "Lottery",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
