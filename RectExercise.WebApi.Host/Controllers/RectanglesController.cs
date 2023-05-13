using Microsoft.AspNetCore.Mvc;
using RectExercise.WebApi.Host.DTO;

namespace RectExercise.WebApi.Host.Controllers
{
    [ApiController]
    [Route("rectangles")]
    public class RectanglesController : ControllerBase
    {
        private readonly ILogger<RectanglesController> _logger;

        public RectanglesController(ILogger<RectanglesController> logger)
        {
            _logger = logger;
        }

        [HttpPost("find-by-points")]
        public IEnumerable<PointMatchDto> GetByMatchingPoints(IReadOnlyList<PointDto> points)
        {
            return points
                .Take(Random.Shared.Next(points.Count))
                .Select(x => new PointMatchDto(x, new[] {
                    new RectangleDto(
                        new PointDto(1, 2),
                        new PointDto(4, 5))
                }))
                .ToList();
        }
    }
}