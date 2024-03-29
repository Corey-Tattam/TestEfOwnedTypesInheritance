﻿//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Domain.Entities;
//using TestEfOwnedTypesInheritance.Helpers;
//using Application.Interfaces.Persistence;

//namespace TestEfOwnedTypesInheritance.EntityFrameworkDemos
//{
//    public class EntityFrameworkCoreFivePointZeroDemo : BaseEntityFrameworkDemoCollection
//    {
//        private const string EfCoreFivePointZeroOrderIdentifier = "EfCore5Demo";

//        public EntityFrameworkCoreFivePointZeroDemo(IServiceProvider serviceProvider) : base(serviceProvider)
//        {
//        }

//        public override async Task DemoAsync()
//        {
//            await ExecuteDemoMethodsWithScopedContextAsync(
//                EfCoreFivePointZeroDemo_SimpleLoggingAsync, 
//                EfCoreFivePointZeroDemo_FilteredIncludeAsync
//            );
//        }

//        private static Task EfCoreFivePointZeroDemo_SimpleLoggingAsync(ITestDbContext dbContext)
//        {
//            ConsoleLoggingHelper.WriteTestSeparator(nameof(EfCoreFivePointZeroDemo_SimpleLoggingAsync));

//            var generatedSql = dbContext.Documents
//                .Where(d => !d.IsDeleted)
//                .ToQueryString();

//            Console.WriteLine(generatedSql);

//            return Task.CompletedTask;
//        }

//        private static async Task EfCoreFivePointZeroDemo_FilteredIncludeAsync(ITestDbContext dbContext)
//        {
//            ConsoleLoggingHelper.WriteTestSeparator(nameof(EfCoreFivePointZeroDemo_FilteredIncludeAsync));

//            /*
//            INSERT INTO [dbo].[Orders] ([OrderType], [Identifier])
//                VALUES (0, 'EfCore5Demo')
//            ;
//            DECLARE @OrderId INT = SCOPE_IDENTITY();

//            INSERT INTO [dbo].[Consumers] ([OrderId], [IsOrganisation], [Street], [Suburb], [State], [OrganisationName], [IndividualName], [CompanyType])
//                VALUES
//                    (@OrderId, 0, 'street1', 'suburb1', 'state1', NULL, 'First Individual', NULL)
//                    ,(@OrderId, 0, 'street2', 'suburb2', 'state2', NULL, 'Second Individual', NULL)
//                    ,(@OrderId, 1, 'street3', 'suburb3', 'state3', 'First Organisation', NULL, 'Company   ')
//                    ,(@OrderId, 1, 'street4', 'suburb3', 'state4', 'Second Organisation', NULL, 'SoleTrader')
//                    ,(@OrderId, 1, 'street5', 'suburb5', 'state5', 'Third Organisation', NULL, 'Company   ')
//            ;

//            INSERT INTO [dbo].[Documents] ([OrderId], [FilePath], [IsDeleted])
//                VALUES
//                    (@OrderId, 'FilePath_1_NotDeleted', 0)
//                    ,(@OrderId, 'FilePath_2_Deleted', 1)
//                    ,(@OrderId, 'FilePath_3_NotDeleted', 0) 
//            ;
//             */


//            // ----------------------------------------
//            // -- Previous Method

//            var previousGetOrderQuery = dbContext.SettlementOrders.AsNoTracking()
//                .Include(o => o.Documents)
//                .Include(o => o.IndividualConsumers)
//                .Include(o => o.OrganisationalConsumers)
//                .Where(o => o.Identifier == EfCoreFivePointZeroOrderIdentifier);

//            var previousGetOrderQueryString = previousGetOrderQuery.ToQueryString();
//            Console.WriteLine("Previous Method SQL:");
//            Console.WriteLine(previousGetOrderQueryString);

//            var orderFromPreviousQuery = await previousGetOrderQuery.FirstAsync();


//            // ----------------------------------------
//            // -- Filtered Includes Method

//            var newGetOrderQuery = dbContext.SettlementOrders.AsNoTracking()
//                .Include(o => o.Documents.Where(d => !d.IsDeleted))
//                .Include(o => o.IndividualConsumers.Where(ic => ic.IndividualName == "Second Individual"))
//                .Include(o => o.OrganisationalConsumers.Where(oc => oc.CompanyType == CompanyType.Company))
//                .Where(o => o.Identifier == EfCoreFivePointZeroOrderIdentifier);

//            var newGetOrderQueryString = newGetOrderQuery.ToQueryString();
//            Console.WriteLine();
//            Console.WriteLine("Filtered Includes SQL:");
//            Console.WriteLine(newGetOrderQueryString);

//            var orderFromNewQuery = await newGetOrderQuery.FirstAsync();

//            //var jsonSerializerOptions = new JsonSerializerOptions
//            //{
//            //    ReferenceHandler = ReferenceHandler.Preserve
//            //};

//            //Console.WriteLine(JsonSerializer.Serialize(orderFromPreviousQuery, jsonSerializerOptions));
//            //Console.WriteLine(JsonSerializer.Serialize(newGetOrderQuery, jsonSerializerOptions));

//        }

//    }
//}
