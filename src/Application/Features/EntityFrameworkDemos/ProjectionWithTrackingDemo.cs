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
    /// Show that it's possible to project and track at the same time.
    /// 
    /// Sometimes you want to change a sub-resource but you may need some details about the parent.
    /// The key point here is "some details". We need not load the entire parent object.
    /// </summary>
    public class ProjectionWithTrackingDemo : IEntityFrameworkFeatureDemo
    {
        private const string EfCoreFivePointZeroOrderIdentifier = "Identifier_Seed_1";

        public string FeatureDescription => "Filtered Includes";

        public async Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation)
        {
            // Sometimes you want to change a sub-resource but you may need some details about the parent.
            var trackedOrderDetails = await dbContext.SettlementOrders
                .Where(o => o.Identifier == EfCoreFivePointZeroOrderIdentifier)
                .Select(o => new
                {
                    o.SettlementStringFieldOne,
                    o.OrganisationalConsumers
                })
                .FirstAsync(cancellation);

            var firstConsumer = trackedOrderDetails.OrganisationalConsumers.First();

            Console.WriteLine($"PRE SAVE: First Consumer.ConsumerStringFieldOne = {firstConsumer.ConsumerStringFieldOne}");

            firstConsumer.ConsumerStringFieldOne += trackedOrderDetails.SettlementStringFieldOne;
            await dbContext.SaveChangesAsync(cancellation);

            Console.WriteLine($"POST SAVE: First Consumer.ConsumerStringFieldOne = {firstConsumer.ConsumerStringFieldOne}");


            /* SQL to verify:
              SELECT * 
                FROM [dbo].[Consumers] c 
                    JOIN [dbo].[Orders] o ON c.[OrderId] = o.[Id]
                WHERE o.[Identifier] = 'EfCore5Demo'
              ;
             */
        }
    }
}
