using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    public partial class AddTournamentPlayerFlagsAndTournamentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add tournament_type to tournaments
            migrationBuilder.AddColumn<string>(
                name: "tournament_type",
                table: "tournaments",
                type: "text",
                nullable: true);

            // Add columns to tournament_players
            migrationBuilder.AddColumn<bool>(
                name: "tp_disqualified",
                table: "tournament_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "tp_bye",
                table: "tournament_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "tp_dropped",
                table: "tournament_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "tp_position",
                table: "tournament_players",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tournament_type",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tp_disqualified",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_bye",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_dropped",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_position",
                table: "tournament_players");
        }
    }
}
