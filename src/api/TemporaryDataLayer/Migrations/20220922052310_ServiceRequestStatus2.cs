using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ServiceRequestStatus2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.serviceRequestStatus",
                table: "serviceRequestStatus");

            migrationBuilder.RenameTable(
                name: "serviceRequestStatus",
                newName: "ServiceRequestStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.ServiceRequestStatus",
                table: "ServiceRequestStatus",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.ServiceRequestStatus",
                table: "ServiceRequestStatus");

            migrationBuilder.RenameTable(
                name: "ServiceRequestStatus",
                newName: "serviceRequestStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.serviceRequestStatus",
                table: "serviceRequestStatus",
                column: "Id");
        }
    }
}
