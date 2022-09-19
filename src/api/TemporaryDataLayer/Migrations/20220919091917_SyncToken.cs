using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class SyncToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SyncTokens",
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
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SyncTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.SyncTokens_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "SyncTokens",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncTokens");
        }
    }
}
