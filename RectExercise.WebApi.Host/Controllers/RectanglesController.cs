using Microsoft.AspNetCore.Mvc;
using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Contract.Services;

namespace RectExercise.WebApi.Host.Controllers
{
    [ApiController]
    [Route("rectangles")]
    public class RectanglesController : ControllerBase
    {
        private readonly IRectanglesService _rectanglesService;

        public RectanglesController(
            IRectanglesService rectanglesService)
        {
            _rectanglesService = rectanglesService;
        }

        [HttpPost("find-by-points")]
        public Task<IEnumerable<PointMatchDto>> GetByMatchingPoints(IReadOnlyList<PointDto> points, CancellationToken cancellationToken)
            => _rectanglesService.GetRectanglesByMatchingPointsAsync(points, cancellationToken);
    }
}