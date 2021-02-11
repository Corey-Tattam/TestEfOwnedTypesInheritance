using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestEfOwnedTypesInheritance.Entities;
using TestEfOwnedTypesInheritance.Helpers;

namespace TestEfOwnedTypesInheritance.EntityFrameworkDemos
{

    public class EntityFrameworkCoreThreePointOneDemo : BaseEntityFrameworkDemo
    {
        private const string EfDemoOrderIdentifier = "EfCore3DemoA";

        public EntityFrameworkCoreThreePointOneDemo(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
        }

        public override async Task DemoAsync()
        {
            await ExecuteDemoMethodWithScopedContextAsync((db) => EfCoreDemo_RestrictedClientEvaluation(db));
            await EfCoreDemo_ReferenceStitchingChanges(_serviceProvider);
        }

        private static async Task EfCoreDemo_RestrictedClientEvaluation(ITestDbContext dbContext)
        {
            var expectedExceptionThrown = false;
            var unexpectedExceptionThrown = false;

            try
            {
                // This will fail because EF can't translate 'HasCorrectIdentifierForDemo(o.Identifier)' into SQL.
                var result = await dbContext.SettlementOrders.AsNoTracking()
                    .Where(o => o.Identifier.Contains("EF") && HasCorrectIdentifierForDemo(o.Identifier))
                    .Select(o => o.Id)
                    .ToListAsync();
            }
            catch (InvalidOperationException translationFailedException)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("-- EfCoreDemo_RestrictedClientEvaluation");
                Console.WriteLine();
                Console.WriteLine(translationFailedException.Message);
                Console.WriteLine();

                expectedExceptionThrown = true;
            }

            // If we really need to get this to evaluate on the Client side, then we need to instruct EF to bring 
            // the records into memory for further processing, like so:

            // ----------------------------------------
            // -- Synchronously

            try
            {
                var results = dbContext.SettlementOrders.AsNoTracking()
                    .Where(o => o.Identifier.Contains("Ef")) // <-- Processed on the Server (SQL) - i.e. Translates to SQL
                    .AsEnumerable() // <-- This will force the records to load into memory after the SQL component is finished.
                    .Where(o => HasCorrectIdentifierForDemo(o.Identifier)) // <-- Processed Client Side (C#)
                    .Select(o => o.Id)
                    .ToList(); // Yes, this is NOT Async. And that's by design.

                if (results.Count != 1) throw new Exception("Invalid Data - EfCoreDemo_RestrictedClientEvaluation");
            }
            catch (Exception)
            {
                unexpectedExceptionThrown = true;
            }

            // ----------------------------------------
            // -- Asynchronously

            try
            {
                var results = new List<int>();
                var ordersQuery = dbContext.SettlementOrders.AsNoTracking()
                    .Where(o => o.Identifier.Contains("Ef")); // <-- Processed on the Server (SQL) - i.e. Translates to SQL

                await foreach (var order in ordersQuery.AsAsyncEnumerable())
                {
                    if (HasCorrectIdentifierForDemo(order.Identifier))
                    {
                        results.Add(order.Id);
                    }
                }

                if (results.Count != 1) throw new Exception("Invalid Data - EfCoreDemo_RestrictedClientEvaluation");
            }
            catch (Exception)
            {
                unexpectedExceptionThrown = true;
            }

            if (unexpectedExceptionThrown || !expectedExceptionThrown) ConsoleLoggingHelper.WriteTestResult(nameof(EfCoreDemo_RestrictedClientEvaluation), passed: false);
            else ConsoleLoggingHelper.WriteTestResult(nameof(EfCoreDemo_RestrictedClientEvaluation), passed: true);
        }

        private static async Task EfCoreDemo_ReferenceStitchingChanges(IServiceProvider serviceProvider)
        {
            var expectedExceptionThrown = false;
            var unexpectedExceptionThrown = false;

            // ----------------------------------------
            // -- Incorrect Method

            try
            {
                using var scope = serviceProvider.CreateScope();
                using var dbContext = scope.ServiceProvider.GetRequiredService<ITestDbContext>();

                var order = new SettlementOrder();
                var document = new Document
                {
                    OrderId = order.Id,
                    FilePath = "EfCoreDemo_ReferenceStitchingChanges_Incorrect"
                };

                dbContext.Documents.Add(document);
                dbContext.SettlementOrders.Add(order);

                await dbContext.SaveChangesAsync(CancellationToken.None);
            }
            catch (Exception)
            {
                expectedExceptionThrown = true;
            }

            // ----------------------------------------
            // -- Correct Method

            try
            {
                using var scope = serviceProvider.CreateScope();
                using var dbContext = scope.ServiceProvider.GetRequiredService<ITestDbContext>();

                var order = new SettlementOrder();
                var document = new Document
                {
                    Order = order,
                    FilePath = "EfCoreDemo_ReferenceStitchingChanges_Success"
                };

                dbContext.Documents.Add(document);
                dbContext.SettlementOrders.Add(order);

                await dbContext.SaveChangesAsync(CancellationToken.None);
            }
            catch (Exception)
            {
                unexpectedExceptionThrown = true;
            }

            if (unexpectedExceptionThrown || !expectedExceptionThrown) ConsoleLoggingHelper.WriteTestResult(nameof(EfCoreDemo_ReferenceStitchingChanges), passed: false);
            else ConsoleLoggingHelper.WriteTestResult(nameof(EfCoreDemo_ReferenceStitchingChanges), passed: true);
        }

        private static bool HasCorrectIdentifierForDemo(string identifier) => identifier == EfDemoOrderIdentifier;

    }
}
