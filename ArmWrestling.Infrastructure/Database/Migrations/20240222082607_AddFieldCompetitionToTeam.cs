using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldCompetitionToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetitionId",
                table: "Teams",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Competitions_CompetitionId",
                table: "Teams",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competitions_CompetitionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CompetitionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Teams");
        }
    }
}
