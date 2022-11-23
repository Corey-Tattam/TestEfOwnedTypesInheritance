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
    /// Show the benefits of Server side evaluation for Aggregate calculations - i.e. Count/Average through grouping.
    /// 
    /// This example attempts to work out the average number of Documents per Order.
    /// </summary>
    public class AggregateDemo : IEntityFrameworkFeatureDemo
    {
        public string FeatureDescription => "Aggregate Demo";

        public async Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation)
        {

            // ----------------------------------------
            // -- ClientSideEvaluationNoProjectionAndNoTracking

            var averageDocumentsPerOrder = await ClientSideEvaluationNoProjectionAndNoTrackingAsync(dbContext, cancellation);
            Console.WriteLine($"Average Documents per Order: {averageDocumentsPerOrder}");


            // ----------------------------------------
            // -- ClientSideEvaluationWithProjectionAndNoTracking

            averageDocumentsPerOrder = await ClientSideEvaluationWithProjectionAndNoTrackingAsync(dbContext, cancellation);
            Console.WriteLine($"Average Documents per Order: {averageDocumentsPerOrder}");

            // ----------------------------------------
            // -- ServerSideEvaluationNoTracking

            averageDocumentsPerOrder = await ServerSideEvaluationNoTrackingAsync(dbContext, cancellation);
            Console.WriteLine($"Average Documents per Order: {averageDocumentsPerOrder}");

        }

        /// <remarks>
        /// SELECT [d].[Id], [d].[DocumentDateFieldOne], [d].[DocumentDateFieldTwo], [d].[DocumentStringFieldOne], [d].[DocumentStringFieldThree], [d].[DocumentStringFieldTwo], [d].[FilePath], [d].[IsDeleted], [d].[OrderId]
        /// FROM[Documents] AS [d]
        /// </remarks>
        private static async Task<double> ClientSideEvaluationNoProjectionAndNoTrackingAsync(ITestDbContext dbContext, CancellationToken cancellationToken)
        {
            var documents = await dbContext.Documents
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .ToListAsync(cancellationToken);

            return documents.GroupBy(d => d.OrderId)
                .Select(grp => grp.Count())
                .Average();
        }

        /// <remarks>
        /// SELECT [d].[OrderId]
        /// FROM[Documents] AS [d]
        /// </remarks>
        private static async Task<double> ClientSideEvaluationWithProjectionAndNoTrackingAsync(ITestDbContext dbContext, CancellationToken cancellationToken)
        {
            var documents = await dbContext.Documents
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Select(d => d.OrderId)
                .ToListAsync(cancellationToken);

            return documents.GroupBy(OrderId => OrderId)
                .Select(grp => grp.Count())
                .Average();
        }

        /// <remarks>
        /// SELECT AVG(CAST([t].[c] AS float))
        /// FROM(
        ///    SELECT COUNT(*) AS [c]
        ///    FROM [Documents] AS [d]
        ///    GROUP BY [d].[OrderId]
        /// ) AS [t]
        /// </remarks>
        private static Task<double> ServerSideEvaluationNoTrackingAsync(ITestDbContext dbContext, CancellationToken cancellationToken)
        {
            return dbContext.Documents
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .GroupBy(d => d.OrderId)
                .Select(grp => grp.Count())
                .AverageAsync(cancellationToken);
        }

    }
}
