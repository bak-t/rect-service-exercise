using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Contract.Services;
using RectExercise.Data.Contract.Models;
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
            var rectangles = (await _rectanglesRepository.GetRectanglesByMatchingPointsAsync(points, cancellationToken))
                .Select(ConvertToRectangleDto)
                .ToList();

            return points
                .Select(point => new PointMatchDto(
                    Point: point,
                    MatchingRectangles: rectangles
                        .Where(rect => point.X >= rect.TopLeft.X && point.X <= rect.BottomRight.X &&
                            point.Y >= rect.TopLeft.Y && point.Y <= rect.BottomRight.Y)
                        .ToList()));
        }

        private static RectangleDto ConvertToRectangleDto(Rectangle rect)
        {
            return new RectangleDto(
                Id: rect.Id,
                TopLeft: new PointDto(X: rect.Left, Y: rect.Top),
                BottomRight: new PointDto(X: rect.Right, Y: rect.Bottom));
        }
    }
}
