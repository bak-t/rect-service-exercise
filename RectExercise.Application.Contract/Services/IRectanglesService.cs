using RectExercise.Application.Contract.DTO;

namespace RectExercise.Application.Contract.Services
{
    public interface IRectanglesService
    {
        Task<IEnumerable<PointMatchDto>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken);
    }
}
