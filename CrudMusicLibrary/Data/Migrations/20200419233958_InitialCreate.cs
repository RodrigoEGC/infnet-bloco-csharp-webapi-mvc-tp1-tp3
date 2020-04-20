using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicalGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(maxLength: 20, nullable: false),
                    MusicalGenre = table.Column<string>(maxLength: 60, nullable: false),
                    Beginnings = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(maxLength: 25, nullable: false),
                    Nation = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalGroups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "discographies",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    GroupEntityGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discographies", x => x.AlbumId);
                    table.ForeignKey(
                        name: "FK_discographies_MusicalGroups_GroupEntityGroupId",
                        column: x => x.GroupEntityGroupId,
                        principalTable: "MusicalGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discographies_GroupEntityGroupId",
                table: "discographies",
                column: "GroupEntityGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discographies");

            migrationBuilder.DropTable(
                name: "MusicalGroups");
        }
    }
}
