using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class AddTournamentSquashVenue_BridgeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_SquashVenues_SquashVenueId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_SquashVenueId",
                table: "Fixtures");

            migrationBuilder.RenameColumn(
                name: "SquashVenueId",
                table: "Fixtures",
                newName: "TournamentSquashVenueId");

            migrationBuilder.CreateTable(
                name: "TournamentSquashVenues",
                columns: table => new
                {
                    TournamentSquashVenueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SquashVenueId = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentSquashVenues", x => x.TournamentSquashVenueId);
                    table.ForeignKey(
                        name: "FK_TournamentSquashVenues_SquashVenues_SquashVenueId",
                        column: x => x.SquashVenueId,
                        principalTable: "SquashVenues",
                        principalColumn: "SquashVenueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentSquashVenues_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentSquashVenues_SquashVenueId",
                table: "TournamentSquashVenues",
                column: "SquashVenueId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentSquashVenues_TournamentId",
                table: "TournamentSquashVenues",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentSquashVenues");

            migrationBuilder.RenameColumn(
                name: "TournamentSquashVenueId",
                table: "Fixtures",
                newName: "SquashVenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_SquashVenueId",
                table: "Fixtures",
                column: "SquashVenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_SquashVenues_SquashVenueId",
                table: "Fixtures",
                column: "SquashVenueId",
                principalTable: "SquashVenues",
                principalColumn: "SquashVenueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
