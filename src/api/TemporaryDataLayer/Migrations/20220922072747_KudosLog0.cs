using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class KudosLog0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KudosLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    KudosTypeName = table.Column<string>(nullable: true),
                    KudosTypeValue = table.Column<decimal>(nullable: false),
                    KudosSystemType = table.Column<int>(nullable: false, defaultValue: 1),
                    Status = table.Column<int>(nullable: false),
                    Points = table.Column<decimal>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    MultiplyBy = table.Column<int>(nullable: false),
                    KudosBasketId = table.Column<int>(nullable: true),
                    RejectionMessage = table.Column<string>(nullable: true),
                    PictureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.KudosLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.KudosLogs_dbo.ApplicationUser_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.KudosLogs_dbo.KudosBaskets_KudosBasketId",
                        column: x => x.KudosBasketId,
                        principalTable: "KudosBaskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.KudosLogs_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeId",
                table: "KudosLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosBasketId",
                table: "KudosLogs",
                column: "KudosBasketId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "KudosLogs",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KudosLogs");
        }
    }
}
