using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "tournament_cancelled",
                table: "tournaments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "tournament_finished",
                table: "tournaments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "tournament_started",
                table: "tournaments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "tournament_type",
                table: "tournaments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "tp_bye",
                table: "tournament_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "tp_disqualified",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tournament_cancelled",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tournament_finished",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tournament_started",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tournament_type",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tp_bye",
                table: "tournament_players");

            migrationBuilder.DropColumn(
                name: "tp_disqualified",
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
