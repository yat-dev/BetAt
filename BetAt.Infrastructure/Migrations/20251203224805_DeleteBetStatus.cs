using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetAt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBetStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
