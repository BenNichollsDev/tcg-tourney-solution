using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tp_player_swiss_match_points",
                table: "tournament_players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tp_player_swiss_points",
                table: "tournament_players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tp_player_swiss_score",
                table: "tournament_players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tp_player_swiss_wins",
                table: "tournament_players",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tp_player_swiss_match_points",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_player_swiss_points",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_player_swiss_score",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_player_swiss_wins",
                table: "tournament_players");
        }
    }
}
