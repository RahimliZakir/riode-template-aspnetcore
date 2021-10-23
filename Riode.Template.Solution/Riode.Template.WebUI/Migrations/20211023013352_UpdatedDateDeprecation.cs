using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode.Template.WebUI.Migrations
{
    public partial class UpdatedDateDeprecation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Colors");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Colors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Colors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Colors");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: true);
        }
    }
}
