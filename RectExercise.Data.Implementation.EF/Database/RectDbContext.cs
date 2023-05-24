using Microsoft.EntityFrameworkCore;
using RectExercise.Data.Contract.Models;

namespace RectExercise.Data.Implementation.EF.Database
{
    public class RectDbContext : DbContext
    {
        public RectDbContext(DbContextOptions<RectDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<Rectangle> Rectangles { get; set; }
    }
}
