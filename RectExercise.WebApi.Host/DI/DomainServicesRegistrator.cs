using RectExercise.Domain.Contract.Services;
using RectExercise.Domain.Implementation.Services;

namespace RectExercise.WebApi.Host.DI
{
    internal static class DomainServicesRegistrator
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            return services.AddTransient<IRectanglesDomainService, RectanglesDomainService>();
        }
    }
}
