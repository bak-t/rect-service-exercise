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
        /// 
        /// Note that bigger values of <see cref="BatchSize"/> minimize DB roundtrips,
        /// but when &gt;1 SQL Server performs less efficient index scan instead of index seek.
        /// </remarks>
        public int BatchSize { get; set; }
    }
}
