using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetAt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsCalculatedToMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PointsCalculated",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsCalculated",
                table: "Matches");
        }
    }
}
