using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tp_bye",
                table: "tournament_players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "tp_bye",
                table: "tournament_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
