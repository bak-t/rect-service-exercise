using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Contract.Interfaces;

namespace RectExercise.Application.Implementation.Services
{
    public class RectanglesService : IRectanglesService
    {
        public Task<IEnumerable<PointMatchDto>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points)
        {
            return Task.FromResult<IEnumerable<PointMatchDto>>(
                points
                    .Take(Random.Shared.Next(points.Count))
                    .Select(x => new PointMatchDto(x, new[] {
                        new RectangleDto(
                            new PointDto(1, 2),
                            new PointDto(4, 5))
                    }))
                    .ToList());
        }
    }
}
