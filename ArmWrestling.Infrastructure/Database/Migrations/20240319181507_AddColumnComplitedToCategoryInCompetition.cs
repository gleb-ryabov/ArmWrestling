using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnComplitedToCategoryInCompetition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duels_Persons_LooserId",
                table: "Duels");

            migrationBuilder.DropForeignKey(
                name: "FK_Duels_Persons_WinnerId",
                table: "Duels");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_CategoryInCompetitions_CategoryInCompetitionId",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "isBusy",
                table: "Tables",
                newName: "IsBusy");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryInCompetitionId",
                table: "Tables",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Duels",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "LooserId",
                table: "Duels",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Complited",
                table: "CategoryInCompetitions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Duels_Persons_LooserId",
                table: "Duels",
                column: "LooserId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Duels_Persons_WinnerId",
                table: "Duels",
                column: "WinnerId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_CategoryInCompetitions_CategoryInCompetitionId",
                table: "Tables",
                column: "CategoryInCompetitionId",
                principalTable: "CategoryInCompetitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duels_Persons_LooserId",
                table: "Duels");

            migrationBuilder.DropForeignKey(
                name: "FK_Duels_Persons_WinnerId",
                table: "Duels");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_CategoryInCompetitions_CategoryInCompetitionId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Complited",
                table: "CategoryInCompetitions");

            migrationBuilder.RenameColumn(
                name: "IsBusy",
                table: "Tables",
                newName: "isBusy");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryInCompetitionId",
                table: "Tables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Duels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LooserId",
                table: "Duels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Duels_Persons_LooserId",
                table: "Duels",
                column: "LooserId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Duels_Persons_WinnerId",
                table: "Duels",
                column: "WinnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_CategoryInCompetitions_CategoryInCompetitionId",
                table: "Tables",
                column: "CategoryInCompetitionId",
                principalTable: "CategoryInCompetitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
