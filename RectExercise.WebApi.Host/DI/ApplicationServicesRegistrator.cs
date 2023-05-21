using RectExercise.Application.Contract.Services;
using RectExercise.Application.Implementation.Services;

namespace RectExercise.WebApi.Host.DI
{
    internal static class ApplicationServicesRegistrator
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services.AddTransient<IRectanglesService, RectanglesService>();
        }
    }
}
