using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertBaseCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Categories for man (min age - 22 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 1, 1, 22, 255, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 2, 1, 22, 255, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 3, 1, 22, 255, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 4, 1, 22, 255, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 5, 1, 22, 255, 75, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 6, 1, 22, 255, 80, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 7, 1, 22, 255, 85, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 8, 1, 22, 255, 90, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 9, 1, 22, 255, 100, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 10, 1, 22, 255, 110, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 11, 1, 22, 255, 255, 1 });

            // Categories for woman (min age - 22 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 12, 0, 22, 255, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 13, 0, 22, 255, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 14, 0, 22, 255, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 15, 0, 22, 255, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 16, 0, 22, 255, 75, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 17, 0, 22, 255, 80, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 18, 0, 22, 255, 255, 1 });

            //Categories for man (19-21 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 19, 1, 19, 21, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 20, 1, 19, 21, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 21, 1, 19, 21, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 22, 1, 19, 21, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 23, 1, 19, 21, 75, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 24, 1, 19, 21, 80, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 25, 1, 19, 21, 85, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 26, 1, 19, 21, 90, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 27, 1, 19, 21, 255, 1 });

            //Categories for woman (19-21 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 28, 0, 19, 21, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 29, 0, 19, 21, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 30, 0, 19, 21, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 31, 0, 19, 21, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 32, 0, 19, 21, 255, 1 });

            //Categories for man (16-18 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 33, 1, 16, 18, 50, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 34, 1, 16, 18, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 35, 1, 16, 18, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 36, 1, 16, 18, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 37, 1, 16, 18, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 38, 1, 16, 18, 75, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 39, 1, 16, 18, 80, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 40, 1, 16, 18, 255, 1 });

            //Categories for woman (16-18 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 41, 0, 16, 18, 45, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 42, 0, 16, 18, 50, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 43, 0, 16, 18, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 44, 0, 16, 18, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 45, 0, 16, 18, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 46, 0, 16, 18, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 47, 0, 16, 18, 255, 1 });

            //Categories for man (14-15 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 48, 1, 14, 15, 45, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 49, 1, 14, 15, 50, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 50, 1, 14, 15, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 51, 1, 14, 15, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 52, 1, 14, 15, 65, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 53, 1, 14, 15, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 54, 1, 14, 15, 255, 1 });

            //Categories for woman (14-15 years)
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 55, 0, 14, 15, 40, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 56, 0, 14, 15, 45, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 57, 0, 14, 15, 50, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 58, 0, 14, 15, 55, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 59, 0, 14, 15, 60, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 60, 0, 14, 15, 70, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 61, 0, 14, 15, 255, 1 });

            //Absolute categories
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 62, 1, 0, 255, 255, 1 });
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Gender", "MinAge", "MaxAge", "MaxWeight", "CategoryGroupId" },
                values: new object[] { 63, 0, 0, 255, 255, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i < 64; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Categories",
                    keyColumn: "Id",
                    keyValue: i);
            }
        }
    }
}
