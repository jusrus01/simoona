using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Skill1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    ShowInAutoComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSkills",
                columns: table => new
                {
                    Skill_Id = table.Column<int>(nullable: false),
                    ApplicationUser_Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ApplicationUserSkills", x => new { x.ApplicationUser_Id, x.Skill_Id });
                    table.ForeignKey(
                        name: "FK_dbo.ApplicationUserSkills_dbo.AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUser_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ApplicationUserSkills_dbo.Skills_SkillId",
                        column: x => x.Skill_Id,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Id",
                table: "ApplicationUserSkills",
                column: "ApplicationUser_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Id",
                table: "ApplicationUserSkills",
                column: "Skill_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Title",
                table: "Skills",
                column: "Title")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSkills");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
