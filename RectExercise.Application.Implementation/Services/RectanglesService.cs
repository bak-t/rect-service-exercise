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

        public async Task<IEnumerable<PointMatchDto>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken)
        {
            var rectangles = await _rectanglesRepository.GetRectanglesByMatchingPointsAsync(points, cancellationToken);
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
