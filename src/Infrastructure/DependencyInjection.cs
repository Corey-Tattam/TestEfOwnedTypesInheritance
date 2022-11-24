using Application.Interfaces.Demos;
using Application.Interfaces.Persistence;
using Infrastructure.Demos;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();
            services.AddLogging(configure => configure.AddSerilog());

            // Services
            services.AddScoped<IDemoCollectionRunner, EntityFrameworkDemoCollectionRunner>();

            // DB Context
            services.AddDbContext<ITestDbContext, TestDbContext>(config =>
            {
                config.UseSqlServer("Server=.\\SQLEXPRESS;Database=TestEfOwnedInheritance;Trusted_Connection=True;MultipleActiveResultSets=true");
                config.EnableSensitiveDataLogging();
            });

            services.AddScoped<TestDbContextInitialiser>();

            return services;
        }
    }
}
