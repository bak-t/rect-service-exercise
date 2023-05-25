using RectExercise.Domain.Contract.Models;

namespace RectExercise.Domain.Contract.Services
{
    public interface IRectanglesDomainService
    {
        bool RectangleContainsPoint(Rectangle rect, int x, int y);
    }
}
