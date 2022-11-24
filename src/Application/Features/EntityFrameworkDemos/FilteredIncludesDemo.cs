using Application.Interfaces.Demos;
using Application.Interfaces.Persistence;
using BenchmarkDotNet.Attributes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.EntityFrameworkDemos
{
    public class FilteredIncludesDemo : IEntityFrameworkFeatureDemo
    {
        private const string EfCoreFivePointZeroOrderIdentifier = "Identifier_Seed_1";

        public string FeatureDescription => "Filtered Includes";

        [Benchmark]
        public async Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation)
        {
            /*
            INSERT INTO [dbo].[Orders] ([OrderType], [Identifier])
                VALUES (0, 'EfCore5Demo')
            ;
            DECLARE @OrderId INT = SCOPE_IDENTITY();

            INSERT INTO [dbo].[Consumers] ([OrderId], [IsOrganisation], [Street], [Suburb], [State], [OrganisationName], [IndividualName], [CompanyType])
                VALUES
                    (@OrderId, 0, 'street1', 'suburb1', 'state1', NULL, 'First Individual', NULL)
                    ,(@OrderId, 0, 'street2', 'suburb2', 'state2', NULL, 'Second Individual', NULL)
                    ,(@OrderId, 1, 'street3', 'suburb3', 'state3', 'First Organisation', NULL, 'Company   ')
                    ,(@OrderId, 1, 'street4', 'suburb3', 'state4', 'Second Organisation', NULL, 'SoleTrader')
                    ,(@OrderId, 1, 'street5', 'suburb5', 'state5', 'Third Organisation', NULL, 'Company   ')
            ;

            INSERT INTO [dbo].[Documents] ([OrderId], [FilePath], [IsDeleted])
                VALUES
                    (@OrderId, 'FilePath_1_NotDeleted', 0)
                    ,(@OrderId, 'FilePath_2_Deleted', 1)
                    ,(@OrderId, 'FilePath_3_NotDeleted', 0) 
            ;
             */


            // ----------------------------------------
            // -- Previous Method

            //var previousGetOrderQuery = dbContext.SettlementOrders.AsNoTracking()
            //    .Include(o => o.Documents)
            //    .Include(o => o.IndividualConsumers)
            //    .Include(o => o.OrganisationalConsumers)
            //    .Where(o => o.Identifier == EfCoreFivePointZeroOrderIdentifier);

            //var previousGetOrderQueryString = previousGetOrderQuery.ToQueryString();
            //Console.WriteLine("Previous Method SQL:");
            //Console.WriteLine(previousGetOrderQueryString);

            //var orderFromPreviousQuery = await previousGetOrderQuery.FirstAsync();


            // ----------------------------------------
            // -- Filtered Includes Method

            var newGetOrderQuery = dbContext.SettlementOrders.AsNoTracking()
                .Include(o => o.Documents.Where(d => !d.IsDeleted))
                .Include(o => o.IndividualConsumers.Where(ic => ic.IndividualName == "IndividualName_Order-1_Consumer-1"))
                .Include(o => o.OrganisationalConsumers.Where(oc => oc.CompanyType == CompanyType.Company))
                .Where(o => o.Identifier == EfCoreFivePointZeroOrderIdentifier);

            var newGetOrderQueryString = newGetOrderQuery.ToQueryString();
            Console.WriteLine();
            Console.WriteLine("Filtered Includes SQL:");
            Console.WriteLine(newGetOrderQueryString);

            var orderFromNewQuery = await newGetOrderQuery.FirstAsync();

            //var jsonSerializerOptions = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};

            //Console.WriteLine(JsonSerializer.Serialize(orderFromPreviousQuery, jsonSerializerOptions));
            //Console.WriteLine(JsonSerializer.Serialize(newGetOrderQuery, jsonSerializerOptions));
        }
    }
}
