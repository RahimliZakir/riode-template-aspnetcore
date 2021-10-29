using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode.Template.WebUI.Migrations
{
    public partial class Several : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecificationCategoryCollections",
                table: "SpecificationCategoryCollections");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationCategoryCollections_CategoryId",
                table: "SpecificationCategoryCollections");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecificationCategoryCollections",
                table: "SpecificationCategoryCollections",
                columns: new[] { "CategoryId", "SpecificationId" });

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationCategoryCollections_SpecificationId",
                table: "SpecificationCategoryCollections",
                column: "SpecificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecificationCategoryCollections",
                table: "SpecificationCategoryCollections");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationCategoryCollections_SpecificationId",
                table: "SpecificationCategoryCollections");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecificationCategoryCollections",
                table: "SpecificationCategoryCollections",
                columns: new[] { "SpecificationId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationCategoryCollections_CategoryId",
                table: "SpecificationCategoryCollections",
                column: "CategoryId");
        }
    }
}
