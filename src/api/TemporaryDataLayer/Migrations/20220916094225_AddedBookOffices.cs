using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class AddedBookOffices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookOfficeId",
                table: "BookLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookOffices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BookOffices_dbo.Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.BookOffices_dbo.Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.BookOffices_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookId",
                table: "BookOffices",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeId",
                table: "BookOffices",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "BookOffices",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs");

            migrationBuilder.DropTable(
                name: "BookOffices");

            migrationBuilder.DropIndex(
                name: "IX_BookOfficeId",
                table: "BookLogs");

            migrationBuilder.DropColumn(
                name: "BookOfficeId",
                table: "BookLogs");
        }
    }
}
