using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RectExercise.Data.Implementation.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToRectangleCoordinates : Migration
    {
        const string IndexName = "IX_Rectangles_Coordinates";
        const string TableName = "Rectangles";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: IndexName,
                table: TableName,
                columns: new[] {
                    "Left",
                    "Top",
                    "Right",
                    "Bottom",
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: IndexName, table: TableName);
        }
    }
}
