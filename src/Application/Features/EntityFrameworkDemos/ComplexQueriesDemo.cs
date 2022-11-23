using Application.Interfaces.Demos;
using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.EntityFrameworkDemos
{
    /// <summary>
    /// Show that it's possible to build sub-queries by referencing the context multiple times.
    /// 
    /// EXAMPLE:
    /// This is a little contrived, for this scenario, we want to retrieve all Orders with 5 or more 
    /// non-deleted documents.
    /// </summary>
    public class ComplexQueriesDemo : IEntityFrameworkFeatureDemo
    {

        public string FeatureDescription => "Filtered Includes";

        public async Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation)
        {

            var ordersWithNumberOfDocuments = await dbContext.SettlementOrders.AsNoTracking()
                .Where(o =>
                    dbContext.Documents
                        .Where(d => !d.IsDeleted)
                        .GroupBy(d => d.OrderId)
                        .Where(grp => grp.Count() >= 5)
                        .Select(grp => grp.Key)
                        .Contains(o.Id))
                .ToListAsync(cancellation);

            Console.WriteLine($"# Orders with 5 or more Documents = {ordersWithNumberOfDocuments.Count}");

            /* Generated SQL:
              SELECT COUNT(*)
                FROM [Orders] AS [o]
                WHERE ([o].[OrderType] = 0) AND EXISTS (
                    SELECT 1
                    FROM [Documents] AS [d]
                    WHERE [d].[IsDeleted] <> CAST(1 AS bit)
                    GROUP BY [d].[OrderId]
                    HAVING (COUNT(*) >= 5) AND ([d].[OrderId] = [o].[Id]))
              ;
             */
        }
    }
}
