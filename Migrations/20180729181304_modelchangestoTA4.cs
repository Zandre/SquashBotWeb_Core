using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class modelchangestoTA4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId1",
                table: "TournamentAdministrators");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "TournamentAdministrators",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentAdministrators_ApplicationUserId1",
                table: "TournamentAdministrators",
                newName: "IX_TournamentAdministrators_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_UserId",
                table: "TournamentAdministrators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_UserId",
                table: "TournamentAdministrators");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TournamentAdministrators",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentAdministrators_UserId",
                table: "TournamentAdministrators",
                newName: "IX_TournamentAdministrators_ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId1",
                table: "TournamentAdministrators",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
