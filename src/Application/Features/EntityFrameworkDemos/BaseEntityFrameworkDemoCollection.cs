//using Application.Interfaces.Demos;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Linq.Expressions;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.Interfaces.Persistence;

//namespace TestEfOwnedTypesInheritance.EntityFrameworkDemos
//{
//    public abstract class BaseEntityFrameworkDemoCollection : IDemoCollectionRunner
//    {
//        private readonly IServiceScopeFactory _serviceScopeFactory;
//        private readonly IDemoLoggerHelper _demoLoggerHelper;

//        protected BaseEntityFrameworkDemoCollection(IServiceScopeFactory serviceScopeFactory, IDemoLoggerHelper demoLoggerHelper)
//        {
//            _serviceScopeFactory = serviceScopeFactory;
//            _demoLoggerHelper = demoLoggerHelper;
//        }

//        protected async Task ExecuteDemoMethodWithScopedContextAsync(Expression<Func<ITestDbContext, Task>> demoMethodExpression)
//        {
//            using var scope = _serviceScopeFactory.CreateScope();
//            using var dbContext = scope.ServiceProvider.GetRequiredService<ITestDbContext>();

//            _demoLoggerHelper.WriteDemoSeparator(demoMethodExpression.)

//            var demoMethodDelegate = demoMethodExpression.Compile();

//            await demoMethodDelegate.Invoke(dbContext);
//        }

//        protected async Task ExecuteDemoMethodsWithScopedContextAsync(params Expression<Func<ITestDbContext, Task>>[] demoMethodDelegates)
//        {
//            foreach(var demo in demoMethodDelegates)
//            {
//                await ExecuteDemoMethodWithScopedContextAsync(demo);
//            }
//        }

//        public abstract Task DemoAsync();
//        public Task RunAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
//    }
//}
