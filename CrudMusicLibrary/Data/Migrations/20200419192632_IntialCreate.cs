using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicalGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(maxLength: 20, nullable: false),
                    MusicalGenre = table.Column<string>(maxLength: 60, nullable: false),
                    Beginnings = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(maxLength: 25, nullable: false),
                    Nation = table.Column<string>(maxLength: 25, nullable: false),
                    RecordCompanyn = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalGroups", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicalGroups");
        }
    }
}
