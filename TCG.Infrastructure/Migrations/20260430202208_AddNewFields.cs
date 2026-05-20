using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "pairing_tp_2",
                table: "pairings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "pairing_tp_1_score",
                table: "pairings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pairing_tp_2_score",
                table: "pairings",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pairing_tp_1_score",
                table: "pairings");

            migrationBuilder.DropColumn(
                name: "pairing_tp_2_score",
                table: "pairings");

            migrationBuilder.AlterColumn<int>(
                name: "pairing_tp_2",
                table: "pairings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
