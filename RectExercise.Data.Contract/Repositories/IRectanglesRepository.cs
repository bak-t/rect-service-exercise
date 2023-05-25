using RectExercise.Application.Contract.DTO;
using RectExercise.Data.Contract.Models;

namespace RectExercise.Data.Contract.Repositories
{
    public interface IRectanglesRepository
    {
        Task<IReadOnlyList<Rectangle>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points, CancellationToken cancellationToken);
    }
}
