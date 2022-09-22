using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ServiceRequestComment0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KudosShopItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PictureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.KudosShopItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.KudosShopItem_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    PriorityId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    KudosAmmount = table.Column<int>(nullable: true),
                    KudosShopItemId = table.Column<int>(nullable: true),
                    PictureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequest_dbo.ApplicationUser_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequest_dbo.KudosShopItem_KudosShopItemId",
                        column: x => x.KudosShopItemId,
                        principalTable: "KudosShopItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequest_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequest_dbo.ServiceRequestPriorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "ServiceRequestPriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequest_dbo.ServiceRequestStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ServiceRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequestComments",
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
                    ServiceRequestId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ServiceRequestComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequestComments_dbo.AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequestComments_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequestComments_dbo.ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "KudosShopItem",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeId",
                table: "ServiceRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosShopItemId",
                table: "ServiceRequest",
                column: "KudosShopItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "ServiceRequest",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityId",
                table: "ServiceRequest",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusId",
                table: "ServiceRequest",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeId",
                table: "ServiceRequestComments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "ServiceRequestComments",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestId",
                table: "ServiceRequestComments",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestComments");

            migrationBuilder.DropTable(
                name: "ServiceRequest");

            migrationBuilder.DropTable(
                name: "KudosShopItem");
        }
    }
}
