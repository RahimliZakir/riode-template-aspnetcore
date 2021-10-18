using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode.Template.WebUI.Migrations
{
    public partial class BrandDeletedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Brands",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Brands");
        }
    }
}
