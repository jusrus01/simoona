using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ServiceRequestCategory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequestCategoryApplicationUsers",
                columns: table => new
                {
                    ServiceRequestCategory_Id = table.Column<int>(nullable: false),
                    ApplicationUser_Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ServiceRequestCategoryApplicationUsers", x => new { x.ServiceRequestCategory_Id, x.ApplicationUser_Id });
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequestCategoryApplicationUsers_dbo.AspNetUsers_ApplicationUser_Id",
                        column: x => x.ApplicationUser_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ServiceRequestCategoryApplicationUsers_dbo.ServiceRequestCategories_ServiceRequestCategory_Id",
                        column: x => x.ServiceRequestCategory_Id,
                        principalTable: "ServiceRequestCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Id",
                table: "ServiceRequestCategoryApplicationUsers",
                column: "ApplicationUser_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestCategory_Id",
                table: "ServiceRequestCategoryApplicationUsers",
                column: "ServiceRequestCategory_Id")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestCategoryApplicationUsers");
        }
    }
}
