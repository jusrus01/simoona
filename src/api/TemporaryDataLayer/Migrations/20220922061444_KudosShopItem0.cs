using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class KudosShopItem0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosShopItem_dbo.Organizations_OrganizationId",
                table: "KudosShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.KudosShopItem_KudosShopItemId",
                table: "ServiceRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.KudosShopItem",
                table: "KudosShopItem");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "KudosShopItem");

            migrationBuilder.RenameTable(
                name: "KudosShopItem",
                newName: "KudosShopItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.KudosShopItems",
                table: "KudosShopItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "KudosShopItems",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosShopItems_dbo.Organizations_OrganizationId",
                table: "KudosShopItems",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.KudosShopItems_KudosShopItemId",
                table: "ServiceRequest",
                column: "KudosShopItemId",
                principalTable: "KudosShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.KudosShopItems_dbo.Organizations_OrganizationId",
                table: "KudosShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.KudosShopItems_KudosShopItemId",
                table: "ServiceRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.KudosShopItems",
                table: "KudosShopItems");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "KudosShopItems");

            migrationBuilder.RenameTable(
                name: "KudosShopItems",
                newName: "KudosShopItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.KudosShopItem",
                table: "KudosShopItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "KudosShopItem",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.KudosShopItem_dbo.Organizations_OrganizationId",
                table: "KudosShopItem",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.KudosShopItem_KudosShopItemId",
                table: "ServiceRequest",
                column: "KudosShopItemId",
                principalTable: "KudosShopItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
