using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Contract.Services;
using RectExercise.Data.Contract.Repositories;

namespace RectExercise.Application.Implementation.Services
{
    public class RectanglesService : IRectanglesService
    {
        private readonly IRectanglesRepository _rectanglesRepository;

        public RectanglesService(IRectanglesRepository rectanglesRepository)
        {
            _rectanglesRepository = rectanglesRepository;
        }

        public async Task<IEnumerable<PointMatchDto>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points)
        {
            /*return Task.FromResult<IEnumerable<PointMatchDto>>(
                points
                    .Take(Random.Shared.Next(points.Count))
                    .Select(x => new PointMatchDto(x, new[] {
                        new RectangleDto(
                            new PointDto(1, 2),
                            new PointDto(4, 5))
                    }))
                    .ToList());*/
            var rectangles = await _rectanglesRepository.GetRectanglesByMatchingPointsAsync(points);
            // TODO: match points & rectangles

            return points
                .Zip(rectangles, (point, rect) => new PointMatchDto(point, new[] {
                        new RectangleDto(
                            new PointDto(rect.Left, rect.Top),
                            new PointDto(rect.Right, rect.Bottom))
                    }));
        }
    }
}
