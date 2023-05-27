namespace RectExercise.Data.Implementation.EF.Configuration
{
    public class RectanglesRepositoryOptions
    {
        /// <summary>
        /// Number of points per query of rectangles.
        /// </summary>
        /// <remarks>
        /// When set to 0 then all passed points put into one query, which may lead to
        /// SQL Server error stating that SQL statement is too long.
        /// When set to 1 then each point is put into separate query.
        /// When set to &gt;1 then points put into separate queries accordingly.
        /// </remarks>
        public int BatchSize { get; set; }
    }
}
