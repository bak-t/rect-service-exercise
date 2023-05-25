namespace RectExercise.Application.Contract.DTO
{
    public record RectangleDto(Guid Id, PointDto TopLeft, PointDto BottomRight);
}
