using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ServiceRequest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.Organizations_OrganizationId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "ServiceRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "ServiceRequests",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ServiceRequests",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KudosAmmount",
                table: "ServiceRequests",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KudosShopItemId",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureId",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeId",
                table: "ServiceRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosShopItemId",
                table: "ServiceRequests",
                column: "KudosShopItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "ServiceRequests",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_PriorityId",
                table: "ServiceRequests",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusId",
                table: "ServiceRequests",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.AspNetUsers_EmployeeId",
                table: "ServiceRequests",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.KudosShopItems_KudosShopItemId",
                table: "ServiceRequests",
                column: "KudosShopItemId",
                principalTable: "KudosShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.Organizations_OrganizationId",
                table: "ServiceRequests",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequests",
                column: "PriorityId",
                principalTable: "ServiceRequestPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestStatus_StatusId",
                table: "ServiceRequests",
                column: "StatusId",
                principalTable: "ServiceRequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.AspNetUsers_EmployeeId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.KudosShopItems_KudosShopItemId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.Organizations_OrganizationId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestStatus_StatusId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_KudosShopItemId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_PriorityId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_StatusId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "KudosAmmount",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "KudosShopItemId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ServiceRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "ServiceRequests",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ServiceRequests",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "ServiceRequests",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.Organizations_OrganizationId",
                table: "ServiceRequests",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
