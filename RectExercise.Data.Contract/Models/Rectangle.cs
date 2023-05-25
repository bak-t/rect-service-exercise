namespace RectExercise.Data.Contract.Models
{
    public class Rectangle : IEntity
    {
        public Guid Id { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }
    }
}
