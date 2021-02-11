using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TestEfOwnedTypesInheritance.EntityFrameworkDemos
{
    public abstract class BaseEntityFrameworkDemo : IEntityFrameworkDemo
    {
        protected readonly IServiceProvider _serviceProvider;

        protected BaseEntityFrameworkDemo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async Task ExecuteDemoMethodWithScopedContextAsync(Func<ITestDbContext, Task> demoMethodDelegate)
        {
            using var scope = _serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ITestDbContext>();

            await demoMethodDelegate.Invoke(dbContext);
        }

        public abstract Task DemoAsync();
    }
}
