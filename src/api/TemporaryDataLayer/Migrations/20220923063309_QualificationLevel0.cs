using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class QualificationLevel0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualificationLevelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QualificationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.QualificationLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.QualificationLevels_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualificationLevelId",
                table: "AspNetUsers",
                column: "QualificationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "QualificationLevels",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ApplicationUser_dbo.QualificationLevels_QualificationLevelId",
                table: "AspNetUsers",
                column: "QualificationLevelId",
                principalTable: "QualificationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ApplicationUser_dbo.QualificationLevels_QualificationLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "QualificationLevels");

            migrationBuilder.DropIndex(
                name: "IX_QualificationLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "QualificationLevelId",
                table: "AspNetUsers");
        }
    }
}
