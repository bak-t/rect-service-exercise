using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RectExercise.Application.Contract.DTO;
using RectExercise.Data.Contract.Repositories;
using RectExercise.Data.Implementation.EF.Configuration;
using RectExercise.Data.Implementation.EF.Database;
using RectExercise.Data.Implementation.EF.Utilities;
using RectExercise.Domain.Contract.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RectExercise.Data.Implementation.EF.Repositories
{
    public class RectanglesRepository : IRectanglesRepository
    {
        private readonly RectDbContext _dbContext;
        private readonly IOptionsSnapshot<RectanglesRepositoryOptions> _options;

        public RectanglesRepository(RectDbContext dbContext, IOptionsSnapshot<RectanglesRepositoryOptions> options)
        {
            _dbContext = dbContext;
            _options = options;
        }

        public async Task<IReadOnlyList<Rectangle>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken)
        {
            return await BatchingUtility
                .WithBatchAsync<PointDto, Rectangle>(
                    points,
                    _options.Value.BatchSize,
                    async batch => await GetRectanglesByMatchingPointsInternalAsync(batch, cancellationToken),
                    cancellationToken)
                .ToListAsync(cancellationToken);
        }

        private async Task<IReadOnlyList<Rectangle>> GetRectanglesByMatchingPointsInternalAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken)
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
