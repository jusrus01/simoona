using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Skill2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_Id",
                table: "ApplicationUserSkills");

            migrationBuilder.DropIndex(
                name: "IX_Skill_Id",
                table: "ApplicationUserSkills");

            migrationBuilder.RenameColumn(
                name: "Skill_Id",
                table: "ApplicationUserSkills",
                newName: "SkillId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUser_Id",
                table: "ApplicationUserSkills",
                newName: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSkills_SkillId",
                table: "ApplicationUserSkills",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserSkills_SkillId",
                table: "ApplicationUserSkills");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "ApplicationUserSkills",
                newName: "Skill_Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserSkills",
                newName: "ApplicationUser_Id");

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
        }
    }
}
