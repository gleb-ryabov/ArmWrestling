using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColoumnsGroupAndTypeDuelToDuels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<char>(
                name: "Group",
                table: "Duels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TypeDuel",
                table: "Duels",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Duels");

            migrationBuilder.DropColumn(
                name: "TypeDuel",
                table: "Duels");
        }
    }
}
