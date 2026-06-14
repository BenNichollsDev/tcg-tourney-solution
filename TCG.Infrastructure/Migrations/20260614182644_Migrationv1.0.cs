using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "leagues",
                columns: table => new
                {
                    league_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    league_name = table.Column<string>(type: "text", nullable: false),
                    league_game = table.Column<string>(type: "text", nullable: false),
                    league_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leagues", x => x.league_id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player_first_name = table.Column<string>(type: "text", nullable: false),
                    player_last_name = table.Column<string>(type: "text", nullable: false),
                    player_email = table.Column<string>(type: "text", nullable: false),
                    player_phone = table.Column<string>(type: "text", nullable: false),
                    player_age = table.Column<DateOnly>(type: "date", nullable: false),
                    player_gender = table.Column<string>(type: "text", nullable: false),
                    player_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.player_id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    staff_first_name = table.Column<string>(type: "text", nullable: false),
                    staff_surname = table.Column<string>(type: "text", nullable: false),
                    staff_email = table.Column<string>(type: "text", nullable: false),
                    staff_password = table.Column<string>(type: "text", nullable: false),
                    staff_mobile = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    tournament_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tournament_seed = table.Column<BigInteger>(type: "numeric", nullable: false),
                    tournament_league = table.Column<int>(type: "integer", nullable: true),
                    tournament_name = table.Column<string>(type: "text", nullable: false),
                    tournament_game = table.Column<string>(type: "text", nullable: false),
                    tournament_format = table.Column<string>(type: "text", nullable: false),
                    tournament_require_deck = table.Column<bool>(type: "boolean", nullable: false),
                    tournament_round_num = table.Column<int>(type: "integer", nullable: true),
                    tournament_max_round_num = table.Column<int>(type: "integer", nullable: true),
                    tournament_description = table.Column<string>(type: "text", nullable: false),
                    tournament_pairing = table.Column<string>(type: "text", nullable: false),
                    tournament_date = table.Column<DateOnly>(type: "date", nullable: false),
                    tournament_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    tournament_entry_fee = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    tournament_max_participants = table.Column<int>(type: "integer", nullable: false),
                    tournament_swiss_topcut = table.Column<bool>(type: "boolean", nullable: false),
                    tournament_swiss_topcut_num = table.Column<int>(type: "integer", nullable: false),
                    tournament_started = table.Column<bool>(type: "boolean", nullable: false),
                    tournament_round_in_progress = table.Column<bool>(type: "boolean", nullable: false),
                    tournament_finished = table.Column<bool>(type: "boolean", nullable: false),
                    tournament_cancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournaments", x => x.tournament_id);
                    table.ForeignKey(
                        name: "FK_tournaments_leagues_tournament_league",
                        column: x => x.tournament_league,
                        principalTable: "leagues",
                        principalColumn: "league_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tournament_players",
                columns: table => new
                {
                    tp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tp_tournament = table.Column<int>(type: "integer", nullable: false),
                    tp_player_name = table.Column<string>(type: "text", nullable: true),
                    player_rr_wins = table.Column<int>(type: "integer", nullable: true),
                    player_rr_draws = table.Column<int>(type: "integer", nullable: true),
                    player_rr_losses = table.Column<int>(type: "integer", nullable: true),
                    player_rr_score = table.Column<int>(type: "integer", nullable: true),
                    player_rr_match_points = table.Column<int>(type: "integer", nullable: true),
                    player_rr_points = table.Column<int>(type: "integer", nullable: true),
                    player_sw_wins = table.Column<int>(type: "integer", nullable: true),
                    player_sw_draws = table.Column<int>(type: "integer", nullable: true),
                    player_sw_losses = table.Column<int>(type: "integer", nullable: true),
                    player_sw_score = table.Column<int>(type: "integer", nullable: true),
                    player_sw_match_points = table.Column<int>(type: "integer", nullable: true),
                    player_sw_points = table.Column<int>(type: "integer", nullable: true),
                    tp_byes = table.Column<int>(type: "integer", nullable: true),
                    games_played = table.Column<int>(type: "integer", nullable: true),
                    tp_matches_played = table.Column<int>(type: "integer", nullable: true),
                    tp_disqualified = table.Column<bool>(type: "boolean", nullable: false),
                    tp_dropped = table.Column<bool>(type: "boolean", nullable: false),
                    tp_position = table.Column<int>(type: "integer", nullable: true),
                    PlayerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournament_players", x => x.tp_id);
                    table.ForeignKey(
                        name: "FK_tournament_players_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "player_id");
                    table.ForeignKey(
                        name: "FK_tournament_players_tournaments_tp_tournament",
                        column: x => x.tp_tournament,
                        principalTable: "tournaments",
                        principalColumn: "tournament_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pairings",
                columns: table => new
                {
                    pairing_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pairing_tournament_id = table.Column<int>(type: "integer", nullable: false),
                    pairing_round_number = table.Column<int>(type: "integer", nullable: true),
                    pairing_tp_1 = table.Column<int>(type: "integer", nullable: false),
                    pairing_tp_2 = table.Column<int>(type: "integer", nullable: true),
                    pairing_tp_1_score = table.Column<int>(type: "integer", nullable: true),
                    pairing_tp_2_score = table.Column<int>(type: "integer", nullable: true),
                    pairing_winner = table.Column<int>(type: "integer", nullable: true),
                    pairing_player_1_game_count = table.Column<int>(type: "integer", nullable: true),
                    pairing_player_2_game_count = table.Column<int>(type: "integer", nullable: true),
                    pairing_has_result = table.Column<bool>(type: "boolean", nullable: false),
                    pairing_draw = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pairings", x => x.pairing_id);
                    table.ForeignKey(
                        name: "FK_pairings_tournament_players_pairing_tp_1",
                        column: x => x.pairing_tp_1,
                        principalTable: "tournament_players",
                        principalColumn: "tp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pairings_tournament_players_pairing_tp_2",
                        column: x => x.pairing_tp_2,
                        principalTable: "tournament_players",
                        principalColumn: "tp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pairings_tournament_players_pairing_winner",
                        column: x => x.pairing_winner,
                        principalTable: "tournament_players",
                        principalColumn: "tp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pairings_tournaments_pairing_tournament_id",
                        column: x => x.pairing_tournament_id,
                        principalTable: "tournaments",
                        principalColumn: "tournament_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pairings_pairing_tournament_id",
                table: "pairings",
                column: "pairing_tournament_id");

            migrationBuilder.CreateIndex(
                name: "IX_pairings_pairing_tp_1",
                table: "pairings",
                column: "pairing_tp_1");

            migrationBuilder.CreateIndex(
                name: "IX_pairings_pairing_tp_2",
                table: "pairings",
                column: "pairing_tp_2");

            migrationBuilder.CreateIndex(
                name: "IX_pairings_pairing_winner",
                table: "pairings",
                column: "pairing_winner");

            migrationBuilder.CreateIndex(
                name: "IX_tournament_players_PlayerId",
                table: "tournament_players",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_tournament_players_tp_tournament",
                table: "tournament_players",
                column: "tp_tournament");

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_tournament_league",
                table: "tournaments",
                column: "tournament_league");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pairings");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "tournament_players");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "leagues");
        }
    }
}
