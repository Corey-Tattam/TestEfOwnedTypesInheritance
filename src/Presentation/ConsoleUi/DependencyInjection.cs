using ConsoleUi.Infrastructure;
using ConsoleUi.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSingleton<IAppHost, AppHost>();
            return services;
        }
    }
}
