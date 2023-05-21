using Microsoft.AspNetCore.Mvc;
using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Contract.Services;

namespace RectExercise.WebApi.Host.Controllers
{
    [ApiController]
    [Route("rectangles")]
    public class RectanglesController : ControllerBase
    {
        private readonly ILogger<RectanglesController> _logger;
        private readonly IRectanglesService _rectanglesService;

        public RectanglesController(
            ILogger<RectanglesController> logger,
            IRectanglesService rectanglesService)
        {
            _logger = logger;
            _rectanglesService = rectanglesService;
        }

        [HttpPost("find-by-points")]
        public Task<IEnumerable<PointMatchDto>> GetByMatchingPoints(IReadOnlyList<PointDto> points)
            => _rectanglesService.GetRectanglesByMatchingPointsAsync(points);
    }
}