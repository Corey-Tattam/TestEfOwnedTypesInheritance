using Application.Interfaces.Demos;
using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.EntityFrameworkDemos
{
    /// <summary>
    /// Demonstrate how a Record can be used with a non-tracking query.
    /// 
    /// Records were introduced in C#9, and complement non-tracking queries well when used as a projected type.
    /// Infact, they can only be used in non-tracking queries - if you try and use this with tracking, entity framework will 
    /// throw an error.
    /// </summary>
    public class ProjectionWithRecordsDemo : IEntityFrameworkFeatureDemo
    {
        public string FeatureDescription => "Filtered Includes";

        public async Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation)
        {

            var orderDetailsRecords = await GetOrderDetailsAsync(dbContext, cancellation);

            Console.WriteLine($"Total number of Records: {orderDetailsRecords.Count}");
        }

        private record OrderDetailsRecord
        {
            public int OrderId { get; init; }

            public OrderType OrderType { get; init; }

            public string Identifier { get; init; } = string.Empty;
        }

        private Task<List<OrderDetailsRecord>> GetOrderDetailsAsync(ITestDbContext dbContext, CancellationToken cancellationToken) =>
            dbContext.SettlementOrders.AsNoTracking()
                .Select(o => new OrderDetailsRecord
                {
                    OrderId = o.Id,
                    OrderType = o.OrderType,
                    Identifier = o.Identifier
                })
                .ToListAsync(cancellationToken);
    }
}
