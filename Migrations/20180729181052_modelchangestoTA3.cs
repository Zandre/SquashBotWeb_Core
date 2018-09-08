using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SquashBotWebCore.Migrations
{
    public partial class modelchangestoTA3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId",
                table: "TournamentAdministrators");

            migrationBuilder.DropIndex(
                name: "IX_TournamentAdministrators_ApplicationUserId",
                table: "TournamentAdministrators");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "TournamentAdministrators",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "TournamentAdministrators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrators_ApplicationUserId1",
                table: "TournamentAdministrators",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId1",
                table: "TournamentAdministrators",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId1",
                table: "TournamentAdministrators");

            migrationBuilder.DropIndex(
                name: "IX_TournamentAdministrators_ApplicationUserId1",
                table: "TournamentAdministrators");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "TournamentAdministrators");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TournamentAdministrators",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdministrators_ApplicationUserId",
                table: "TournamentAdministrators",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentAdministrators_AspNetUsers_ApplicationUserId",
                table: "TournamentAdministrators",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
