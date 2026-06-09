using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tp_player_swiss_wins",
                table: "tournament_players",
                newName: "player_sw_wins");

            migrationBuilder.RenameColumn(
                name: "tp_player_swiss_score",
                table: "tournament_players",
                newName: "player_sw_score");

            migrationBuilder.RenameColumn(
                name: "tp_player_swiss_points",
                table: "tournament_players",
                newName: "player_sw_points");

            migrationBuilder.RenameColumn(
                name: "tp_player_swiss_match_points",
                table: "tournament_players",
                newName: "player_sw_match_points");

            migrationBuilder.RenameColumn(
                name: "tp_player_round_robin_wins",
                table: "tournament_players",
                newName: "player_rr_wins");

            migrationBuilder.RenameColumn(
                name: "tp_player_round_robin_score",
                table: "tournament_players",
                newName: "player_rr_score");

            migrationBuilder.RenameColumn(
                name: "tp_player_round_robin_points",
                table: "tournament_players",
                newName: "player_rr_points");

            migrationBuilder.RenameColumn(
                name: "tp_player_round_robin_match_points",
                table: "tournament_players",
                newName: "player_rr_match_points");

            migrationBuilder.AlterColumn<int>(
                name: "player_sw_points",
                table: "tournament_players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "player_sw_match_points",
                table: "tournament_players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "player_rr_points",
                table: "tournament_players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "player_rr_match_points",
                table: "tournament_players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "games_played",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "games_won",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_received_bye",
                table: "tournament_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "player_rr_draws",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "player_rr_losses",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "player_sw_draws",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "player_sw_losses",
                table: "tournament_players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "pairing_has_result",
                table: "pairings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "pairing_player_1_game_count",
                table: "pairings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pairing_player_2_game_count",
                table: "pairings",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "games_played",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "games_won",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "has_received_bye",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "player_rr_draws",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "player_rr_losses",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "player_sw_draws",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "player_sw_losses",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "pairing_has_result",
                table: "pairings");

            migrationBuilder.DropColumn(
                name: "pairing_player_1_game_count",
                table: "pairings");

            migrationBuilder.DropColumn(
                name: "pairing_player_2_game_count",
                table: "pairings");

            migrationBuilder.RenameColumn(
                name: "player_sw_wins",
                table: "tournament_players",
                newName: "tp_player_swiss_wins");

            migrationBuilder.RenameColumn(
                name: "player_sw_score",
                table: "tournament_players",
                newName: "tp_player_swiss_score");

            migrationBuilder.RenameColumn(
                name: "player_sw_points",
                table: "tournament_players",
                newName: "tp_player_swiss_points");

            migrationBuilder.RenameColumn(
                name: "player_sw_match_points",
                table: "tournament_players",
                newName: "tp_player_swiss_match_points");

            migrationBuilder.RenameColumn(
                name: "player_rr_wins",
                table: "tournament_players",
                newName: "tp_player_round_robin_wins");

            migrationBuilder.RenameColumn(
                name: "player_rr_score",
                table: "tournament_players",
                newName: "tp_player_round_robin_score");

            migrationBuilder.RenameColumn(
                name: "player_rr_points",
                table: "tournament_players",
                newName: "tp_player_round_robin_points");

            migrationBuilder.RenameColumn(
                name: "player_rr_match_points",
                table: "tournament_players",
                newName: "tp_player_round_robin_match_points");

            migrationBuilder.AlterColumn<float>(
                name: "tp_player_swiss_points",
                table: "tournament_players",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "tp_player_swiss_match_points",
                table: "tournament_players",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "tp_player_round_robin_points",
                table: "tournament_players",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "tp_player_round_robin_match_points",
                table: "tournament_players",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
