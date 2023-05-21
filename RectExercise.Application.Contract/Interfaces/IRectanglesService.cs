using RectExercise.Application.Contract.DTO;

namespace RectExercise.Application.Contract.Interfaces
{
    public interface IRectanglesService
    {
        Task<IEnumerable<PointMatchDto>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points);
    }
}
