using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RectExercise.Data.Implementation.EF.Migrations
{
    /// <inheritdoc />
    public partial class SeedRectangleData : Migration
    {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var randomRectangles = GenerateRectanglesData(
                rowCount: 200,
                minX: 1, maxX: 800,
                minY: 1, maxY: 600,
                minWidth: 1, minHeight: 1);
            migrationBuilder.InsertData(
                "Rectangles",
                RectColumns.GetNames(),
                randomRectangles);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new InvalidOperationException("Migration is irreversible since it's impossible to distinguish seeded data and real");
        }

        private object[,] GenerateRectanglesData(
            int rowCount,
            int minX, int maxX,
            int minY, int maxY,
            int minWidth, int minHeight)
        {
            var result = new object[rowCount, RectColumns.GetCount()];

            for (int i = 0; i < rowCount; i++)
            {
                var left = Random.Shared.Next(minX, maxX - minWidth);
                var top = Random.Shared.Next(minY, maxY - minHeight);
                result[i, RectColumns.Left] = left;
                result[i, RectColumns.Top] = top;

                result[i, RectColumns.Right] = Random.Shared.Next(left + 1, maxX);
                result[i, RectColumns.Bottom] = Random.Shared.Next(top + 1, maxY);

                result[i, RectColumns.Id] = Guid.NewGuid();
            }

            return result;
        }

        class RectColumns
        {
            public const int Id = 0;
            public const int Left = 1;
            public const int Top = 2;
            public const int Right = 3;
            public const int Bottom = 4;

            public static string[] GetNames() => _names;
            public static int GetCount() => _names.Length;

            private static readonly string[] _names = new[] { nameof(Id), nameof(Left), nameof(Top), nameof(Right), nameof(Bottom) };
        }
    }
}
