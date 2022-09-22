using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Skill3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserSkills_SkillId",
                table: "ApplicationUserSkills");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserId",
                table: "ApplicationUserSkills",
                column: "ApplicationUserId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_SkillId",
                table: "ApplicationUserSkills",
                column: "SkillId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserId",
                table: "ApplicationUserSkills");

            migrationBuilder.DropIndex(
                name: "IX_SkillId",
                table: "ApplicationUserSkills");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSkills_SkillId",
                table: "ApplicationUserSkills",
                column: "SkillId");
        }
    }
}
