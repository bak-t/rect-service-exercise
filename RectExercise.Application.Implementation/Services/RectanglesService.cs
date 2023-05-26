using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Contract.Services;
using RectExercise.Data.Contract.Repositories;
using RectExercise.Domain.Contract.Models;
using RectExercise.Domain.Contract.Services;

namespace RectExercise.Application.Implementation.Services
{
    public class RectanglesService : IRectanglesService
    {
        private readonly IRectanglesRepository _rectanglesRepository;
        private readonly IRectanglesDomainService _rectangleDomainService;

        public RectanglesService(
            IRectanglesRepository rectanglesRepository,
            IRectanglesDomainService rectangleDomainService)
        {
            _rectanglesRepository = rectanglesRepository;
            _rectangleDomainService = rectangleDomainService;
        }

        public async Task<IEnumerable<PointMatchDto>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken)
        {
            if (points == null) throw new ArgumentNullException(nameof(points));
            if (points.Count == 0) throw new ArgumentException("List of points mustn't be empty", nameof(points));

            var rectangles = await _rectanglesRepository.GetRectanglesByMatchingPointsAsync(points, cancellationToken);

            return points
                .Select(point => new PointMatchDto(
                    Point: point,
                    MatchingRectangles: rectangles
                        .Where(rect => _rectangleDomainService.RectangleContainsPoint(rect, point.X, point.Y))
                        .Select(ConvertToRectangleDto)
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
