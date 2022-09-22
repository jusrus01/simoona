using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ProjectSkill0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Projects_dbo.Skills_SkillId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_SkillId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    Project_Id = table.Column<int>(nullable: false),
                    Skill_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ProjectSkills", x => new { x.Project_Id, x.Skill_Id });
                    table.ForeignKey(
                        name: "FK_dbo.Project2Skill_dbo.Projects_Project2_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Project2Skill_dbo.Skills_Skill_Id",
                        column: x => x.Skill_Id,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_Id",
                table: "ProjectSkills",
                column: "Project_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Id",
                table: "ProjectSkills",
                column: "Skill_Id")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSkills");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillId",
                table: "Projects",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Projects_dbo.Skills_SkillId",
                table: "Projects",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
