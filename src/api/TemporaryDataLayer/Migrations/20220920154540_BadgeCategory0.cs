using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BadgeCategory0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BadgeCategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "KudosType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.KudosType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BadgeCategoryKudosType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalculationPolicyType = table.Column<int>(nullable: false),
                    BadgeCategoryId = table.Column<int>(nullable: false),
                    KudosTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BadgeCategoryKudosType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BadgeCategoryKudosType_dbo.BadgeCategories_BadgeCategoryId",
                        column: x => x.BadgeCategoryId,
                        principalTable: "BadgeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.BadgeCategoryKudosType_dbo.KudosType_KudosTypeId",
                        column: x => x.KudosTypeId,
                        principalTable: "KudosType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BadgeCategoryId",
                table: "BadgeCategoryKudosType",
                column: "BadgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosTypeId",
                table: "BadgeCategoryKudosType",
                column: "KudosTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadgeCategoryKudosType");

            migrationBuilder.DropTable(
                name: "KudosType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BadgeCategories");
        }
    }
}
