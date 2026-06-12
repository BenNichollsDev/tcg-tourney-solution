using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "tournament_players",
                type: "integer",
                nullable: true);

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
                    player_age = table.Column<int>(type: "integer", nullable: false),
                    player_gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.player_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tournament_players_PlayerId",
                table: "tournament_players",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_tournament_players_players_PlayerId",
                table: "tournament_players",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "player_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tournament_players_players_PlayerId",
                table: "tournament_players");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropIndex(
                name: "IX_tournament_players_PlayerId",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "tournament_players");
        }
    }
}
