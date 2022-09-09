using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class OrganizationsAndModuleConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    ShortName = table.Column<string>(maxLength: 64, nullable: false),
                    HostName = table.Column<string>(maxLength: 50, nullable: true),
                    HasRestrictedAccess = table.Column<bool>(nullable: false),
                    WelcomeEmail = table.Column<string>(maxLength: 10000, nullable: false),
                    RequiresUserConfirmation = table.Column<bool>(nullable: false),
                    CalendarId = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    CultureCode = table.Column<string>(nullable: true),
                    BookAppAuthorizationGuid = table.Column<string>(nullable: true),
                    AuthenticationProviders = table.Column<string>(nullable: true),
                    KudosYearlyMultipliers = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOrganization",
                columns: table => new
                {
                    ModuleId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOrganization", x => new { x.ModuleId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_ModuleOrganization_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleOrganization_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOrganization_OrganizationId",
                table: "ModuleOrganization",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleOrganization");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
