using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class TournamentAdminChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrator_Tournament_TournamentId",
                table: "TournamentAdministrator");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "TournamentAdministrator",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TournamentAdministrator",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrator_ApplicationUserId",
                table: "TournamentAdministrator",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrator_AspNetUsers_ApplicationUserId",
                table: "TournamentAdministrator",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrator_Tournament_TournamentId",
                table: "TournamentAdministrator",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrator_AspNetUsers_ApplicationUserId",
                table: "TournamentAdministrator");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrator_Tournament_TournamentId",
                table: "TournamentAdministrator");

            migrationBuilder.DropIndex(
                name: "IX_TournamentAdministrator_ApplicationUserId",
                table: "TournamentAdministrator");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "TournamentAdministrator",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "TournamentAdministrator",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrator_Tournament_TournamentId",
                table: "TournamentAdministrator",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
