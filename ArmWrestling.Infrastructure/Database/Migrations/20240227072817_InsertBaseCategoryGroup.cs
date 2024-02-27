using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertBaseCategoryGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryGroups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Base" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryGroups",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
