using RectExercise.Application.Contract.DTO;
using RectExercise.Data.Contract.Models;
using RectExercise.Data.Contract.Repositories;

namespace RectExercise.Data.Implementation.EF.Repositories
{
    public class RectanglesRepository : IRectanglesRepository
    {
        public Task<IEnumerable<Rectangle>> GetRectanglesByMatchingPointsAsync(IReadOnlyList<PointDto> points)
        {
            return Task.FromResult<IEnumerable<Rectangle>>(
                points
                    .Take(Random.Shared.Next(points.Count))
                    .Select(x => new Rectangle
                    {
                        Left = 1,
                        Top = 2,
                        Right = 4,
                        Bottom = 5,
                    })
                    .ToList());
        }
    }
}
