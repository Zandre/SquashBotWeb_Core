using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class RemoveUnusedColumnInTournamentAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrator_AspNetUsers_ApplicationUserId1",
                table: "TournamentAdministrator");

            migrationBuilder.DropIndex(
                name: "IX_TournamentAdministrator_ApplicationUserId1",
                table: "TournamentAdministrator");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "TournamentAdministrator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "TournamentAdministrator",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrator_ApplicationUserId1",
                table: "TournamentAdministrator",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrator_AspNetUsers_ApplicationUserId1",
                table: "TournamentAdministrator",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
