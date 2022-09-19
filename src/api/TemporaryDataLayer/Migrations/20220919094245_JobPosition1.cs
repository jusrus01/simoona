using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class JobPosition1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.JobPositions_dbo.Organizations_OrganizationId",
                table: "JobPositions");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "JobPositions");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "JobPositions",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.JobPositions_dbo.Organizations_OrganizationId",
                table: "JobPositions",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.JobPositions_dbo.Organizations_OrganizationId",
                table: "JobPositions");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "JobPositions");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "JobPositions",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.JobPositions_dbo.Organizations_OrganizationId",
                table: "JobPositions",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
