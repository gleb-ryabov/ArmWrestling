using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryInCompetitionId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsBusy = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_CategoryInCompetitions_CategoryInCompetitionId",
                        column: x => x.CategoryInCompetitionId,
                        principalTable: "CategoryInCompetitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tables_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_CategoryInCompetitionId",
                table: "Tables",
                column: "CategoryInCompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_CompetitionId",
                table: "Tables",
                column: "CompetitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
