using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interceptors
{
    public class SaveChangesInterceptorOne : SaveChangesInterceptor
    {
        private readonly ILogger<SaveChangesInterceptorOne> _logger;

        public SaveChangesInterceptorOne(ILogger<SaveChangesInterceptorOne> logger)
        {
            _logger = logger;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            TestOne();
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            TestOne();
            return base.SavingChanges(eventData, result);
        }

        private void TestOne()
        {
            _logger.LogInformation("SaveChanges Called on One.");
        }
    }

    public class SaveChangesInterceptorTwo : SaveChangesInterceptor
    {
        private readonly ILogger<SaveChangesInterceptorTwo> _logger;

        public SaveChangesInterceptorTwo(ILogger<SaveChangesInterceptorTwo> logger)
        {
            _logger = logger;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            TestTwo();
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            TestTwo();
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        private void TestTwo()
        {
            _logger.LogInformation("SaveChanges Called on Two.");
        }
    }
}
