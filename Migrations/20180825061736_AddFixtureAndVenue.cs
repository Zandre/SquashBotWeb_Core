using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class AddFixtureAndVenue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SquashVenues",
                columns: table => new
                {
                    SquashVenueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourtsAvailable = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquashVenues", x => x.SquashVenueId);
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    FixtureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Court = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    SquashVenueId = table.Column<int>(nullable: false),
                    TeamAId = table.Column<int>(nullable: false),
                    TeamBId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.FixtureId);
                    table.ForeignKey(
                        name: "FK_Fixtures_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fixtures_SquashVenues_SquashVenueId",
                        column: x => x.SquashVenueId,
                        principalTable: "SquashVenues",
                        principalColumn: "SquashVenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_SectionId",
                table: "Fixtures",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_SquashVenueId",
                table: "Fixtures",
                column: "SquashVenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "SquashVenues");
        }
    }
}
