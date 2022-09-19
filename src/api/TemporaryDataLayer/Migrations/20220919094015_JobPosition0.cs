using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class JobPosition0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobPositionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.JobPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.JobPositions_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPositionId",
                table: "AspNetUsers",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "JobPositions",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ApplicationUser_dbo.JobPositions_JobPositionId",
                table: "AspNetUsers",
                column: "JobPositionId",
                principalTable: "JobPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ApplicationUser_dbo.JobPositions_JobPositionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropIndex(
                name: "IX_JobPositionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobPositionId",
                table: "AspNetUsers");
        }
    }
}
