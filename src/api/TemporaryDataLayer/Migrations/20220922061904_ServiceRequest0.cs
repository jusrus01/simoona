using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ServiceRequest0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.ApplicationUser_EmployeeId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.KudosShopItems_KudosShopItemId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.Organizations_OrganizationId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.ServiceRequestStatus_StatusId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequestComments_dbo.ServiceRequest_ServiceRequestId",
                table: "ServiceRequestComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.ServiceRequest",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_KudosShopItemId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_PriorityId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_StatusId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "KudosAmmount",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "KudosShopItemId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ServiceRequest");

            migrationBuilder.RenameTable(
                name: "ServiceRequest",
                newName: "ServiceRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.ServiceRequests",
                table: "ServiceRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequestComments_dbo.ServiceRequests_ServiceRequestId",
                table: "ServiceRequestComments",
                column: "ServiceRequestId",
                principalTable: "ServiceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.Organizations_OrganizationId",
                table: "ServiceRequests",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequestComments_dbo.ServiceRequests_ServiceRequestId",
                table: "ServiceRequestComments");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.Organizations_OrganizationId",
                table: "ServiceRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.ServiceRequests",
                table: "ServiceRequests");

            migrationBuilder.RenameTable(
                name: "ServiceRequests",
                newName: "ServiceRequest");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KudosAmmount",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KudosShopItemId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.ServiceRequest",
                table: "ServiceRequest",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeId",
                table: "ServiceRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosShopItemId",
                table: "ServiceRequest",
                column: "KudosShopItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityId",
                table: "ServiceRequest",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusId",
                table: "ServiceRequest",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.ApplicationUser_EmployeeId",
                table: "ServiceRequest",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.KudosShopItems_KudosShopItemId",
                table: "ServiceRequest",
                column: "KudosShopItemId",
                principalTable: "KudosShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.Organizations_OrganizationId",
                table: "ServiceRequest",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequest",
                column: "PriorityId",
                principalTable: "ServiceRequestPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequest_dbo.ServiceRequestStatus_StatusId",
                table: "ServiceRequest",
                column: "StatusId",
                principalTable: "ServiceRequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequestComments_dbo.ServiceRequest_ServiceRequestId",
                table: "ServiceRequestComments",
                column: "ServiceRequestId",
                principalTable: "ServiceRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
