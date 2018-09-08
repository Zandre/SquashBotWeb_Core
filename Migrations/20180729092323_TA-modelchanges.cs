using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class TAmodelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentAdministrator");

            migrationBuilder.CreateTable(
                name: "TournamentAdministrators",
                columns: table => new
                {
                    TournamentAdministratorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentAdministrators", x => x.TournamentAdministratorId);
                    table.ForeignKey(
                        name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentAdministrators_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrators_ApplicationUserId",
                table: "TournamentAdministrators",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrators_TournamentId",
                table: "TournamentAdministrators",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentAdministrators");

            migrationBuilder.CreateTable(
                name: "TournamentAdministrator",
                columns: table => new
                {
                    TournamentAdministratorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentAdministrator", x => x.TournamentAdministratorId);
                    table.ForeignKey(
                        name: "FK_TournamentAdministrator_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentAdministrator_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrator_ApplicationUserId",
                table: "TournamentAdministrator",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrator_TournamentId",
                table: "TournamentAdministrator",
                column: "TournamentId");
        }
    }
}
