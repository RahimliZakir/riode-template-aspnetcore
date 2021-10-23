using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode.Template.WebUI.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Sizes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Faqs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Sizes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Faqs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
