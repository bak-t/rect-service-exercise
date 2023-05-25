using RectExercise.Domain.Contract.Models;
using RectExercise.Domain.Contract.Services;

namespace RectExercise.Domain.Implementation.Services
{
    public class RectanglesDomainService : IRectanglesDomainService
    {
        public bool RectangleContainsPoint(Rectangle rect, int x, int y) =>
            x >= rect.Left && x <= rect.Right &&
            y >= rect.Top && y <= rect.Bottom;
    }
}
