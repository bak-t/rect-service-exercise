namespace RectExercise.Application.Contract.DTO
{
    public record PointMatchDto(PointDto Point, IReadOnlyList<RectangleDto> MatchingRectangles);
}
