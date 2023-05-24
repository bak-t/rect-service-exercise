using Microsoft.EntityFrameworkCore;
using RectExercise.Data.Contract.Repositories;
using RectExercise.Data.Implementation.EF.Database;
using RectExercise.Data.Implementation.EF.Repositories;

namespace RectExercise.WebApi.Host.DI
{
    internal static class DataRegistrator
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IRectanglesRepository, RectanglesRepository>()
                .AddDbContext<RectDbContext>((serviceProvider, optionsBuilder) =>
                {
                    var config = serviceProvider.GetRequiredService<IConfiguration>();
                    optionsBuilder.UseSqlServer(config.GetConnectionString("RectDb"));
                });
        }
    }
}
