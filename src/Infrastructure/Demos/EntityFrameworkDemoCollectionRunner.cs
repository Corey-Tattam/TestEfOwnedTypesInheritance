using Application.Interfaces.Demos;
using Application.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Demos
{
    public class EntityFrameworkDemoCollectionRunner : IDemoCollectionRunner
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IEnumerable<IEntityFrameworkFeatureDemo> _demos;

        public EntityFrameworkDemoCollectionRunner(IServiceScopeFactory serviceScopeFactory, IEnumerable<IEntityFrameworkFeatureDemo> demos)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _demos = demos;
        }

        public async Task RunAllAsync(CancellationToken cancellationToken)
        {
            await ExecuteDemoMethodsWithScopedContextAsync(_demos.Select(d => new Func<ITestDbContext, Task>((db) => d.RunAsync(db, cancellationToken))));
        }

        protected async Task ExecuteDemoMethodsWithScopedContextAsync(IEnumerable<Func<ITestDbContext, Task>> demoMethodDelegates)
        {
            foreach (var demo in demoMethodDelegates)
            {
                await ExecuteDemoMethodWithScopedContextAsync(demo);
            }
        }

        protected async Task ExecuteDemoMethodWithScopedContextAsync(Func<ITestDbContext, Task> demoMethodDelegate)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ITestDbContext>();

            await demoMethodDelegate.Invoke(dbContext);
        }
    }
}
