namespace RectExercise.WebApi.Host.DTO
{
    public record PointMatchDto(PointDto Point, IReadOnlyList<RectangleDto> MatchingRectangles);
}
