using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode.Template.WebUI.Migrations
{
    public partial class SubscribeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    SubscribeDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())"),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    EmailConfirmedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribes");
        }
    }
}
