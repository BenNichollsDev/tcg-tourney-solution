using System;
using System.Globalization;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTournamentTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tournament_type",
                table: "tournaments");

            migrationBuilder.RenameColumn(
                name: "games_won",
                table: "tournament_players",
                newName: "tp_matches_played");

            migrationBuilder.AddColumn<BigInteger>(
                name: "tournament_seed",
                table: "tournaments",
                type: "numeric",
                nullable: false,
                defaultValue: BigInteger.Parse("0", NumberFormatInfo.InvariantInfo));

            migrationBuilder.AddColumn<int>(
                name: "tp_byes",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "pairing_draw",
                table: "pairings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tournament_seed",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tp_byes",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "pairing_draw",
                table: "pairings");

            migrationBuilder.RenameColumn(
                name: "tp_matches_played",
                table: "tournament_players",
                newName: "games_won");

            migrationBuilder.AddColumn<string>(
                name: "tournament_type",
                table: "tournaments",
                type: "text",
                nullable: true);
        }
    }
}
