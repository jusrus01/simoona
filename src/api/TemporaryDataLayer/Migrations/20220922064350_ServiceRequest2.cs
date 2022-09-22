using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ServiceRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequests",
                column: "PriorityId",
                principalTable: "ServiceRequestPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ServiceRequests_dbo.ServiceRequestPriorities_PriorityId",
                table: "ServiceRequests",
                column: "PriorityId",
                principalTable: "ServiceRequestPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
