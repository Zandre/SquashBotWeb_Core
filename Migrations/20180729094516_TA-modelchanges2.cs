using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class TAmodelchanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrators_Tournament_TournamentId",
                table: "TournamentAdministrators");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "TournamentAdministrators",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrators_Tournament_TournamentId",
                table: "TournamentAdministrators",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrators_Tournament_TournamentId",
                table: "TournamentAdministrators");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "TournamentAdministrators",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrators_Tournament_TournamentId",
                table: "TournamentAdministrators",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
