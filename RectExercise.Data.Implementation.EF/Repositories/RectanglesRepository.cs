using LinqKit;
using Microsoft.EntityFrameworkCore;
using RectExercise.Application.Contract.DTO;
using RectExercise.Data.Contract.Models;
using RectExercise.Data.Contract.Repositories;
using RectExercise.Data.Implementation.EF.Database;
using System.Linq.Expressions;

namespace RectExercise.Data.Implementation.EF.Repositories
{
    public class RectanglesRepository : IRectanglesRepository
    {
        private readonly RectDbContext _dbContext;

        public RectanglesRepository(RectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Rectangle>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken)
        {
            var containsPointsPredicate = points
                .Select(ConvertToPredicateExpression)
                .Aggregate(PredicateBuilder.New<Rectangle>(), (result, expr) => result.Or(expr));

            return await _dbContext.Rectangles
                .Where(containsPointsPredicate)
                .ToListAsync(cancellationToken);

            static Expression<Func<Rectangle, bool>> ConvertToPredicateExpression(PointDto point) =>
                rect => point.X >= rect.Left && point.X <= rect.Right &&
                    point.Y >= rect.Top && point.Y <= rect.Bottom;
        }
    }
}
