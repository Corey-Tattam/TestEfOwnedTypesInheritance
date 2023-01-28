using Application.Features.EntityFrameworkDemos;
using Application.Interfaces.Demos;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                //.AddScoped<IEntityFrameworkFeatureDemo, FilteredIncludesDemo>()
                .AddScoped<IEntityFrameworkFeatureDemo, ProjectionWithTrackingDemo>()
                .AddScoped<IEntityFrameworkFeatureDemo, ProjectionWithRecordsDemo>()
                .AddScoped<IEntityFrameworkFeatureDemo, AggregateDemo>()
                .AddScoped<IEntityFrameworkFeatureDemo, ComplexQueriesDemo>()
                .AddScoped<IEntityFrameworkFeatureDemo, InterceptorsDemo>()
            ;

            return services;
        }
    }
}
