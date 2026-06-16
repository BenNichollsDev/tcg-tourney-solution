using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tp_round_robin_game_draws",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tp_round_robin_game_losses",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tp_round_robin_game_wins",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tp_swiss_game_draws",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tp_swiss_game_losses",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tp_swiss_game_wins",
                table: "tournament_players",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tp_round_robin_game_draws",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_round_robin_game_losses",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_round_robin_game_wins",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_swiss_game_draws",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_swiss_game_losses",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_swiss_game_wins",
                table: "tournament_players");
        }
    }
}
