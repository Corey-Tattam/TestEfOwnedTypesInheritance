using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Features.Benchmarks
{
    /*
        Show the benefits of using Projection and also how tracking and non-tracking affects the performance.

        EXAMPLE: The scenario here is that we're interested in viewing the Identifier and PropertyAddress.Street data
                 for all Orders in the system. This example shows the various way's that can be done.


        NOTE: The below results for  "WithProjectionAndTracking" and "WithProjectionAndNoTracking" do not show 
              a great deviation, because the projected fields are few and are not trackable anyway.

        // * Summary *

        BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
        Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
        .NET SDK=5.0.401
            [Host]     : .NET 5.0.10 (5.0.1021.41214), X64 RyuJIT
            DefaultJob : .NET 5.0.10 (5.0.1021.41214), X64 RyuJIT


        |                      Method |      Mean |     Error |    StdDev |    Median |     Gen 0 | Allocated |
        |---------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
        |     NoProjectionAndTracking | 30.448 ms | 1.7536 ms | 4.9460 ms | 29.089 ms | 1000.0000 |  7,394 KB |
        |   NoProjectionAndNoTracking | 15.201 ms | 1.1652 ms | 3.4357 ms | 14.896 ms |         - |  4,340 KB |
        |   WithProjectionAndTracking |  3.498 ms | 0.2260 ms | 0.6556 ms |  3.426 ms |         - |    733 KB |
        | WithProjectionAndNoTracking |  3.292 ms | 0.3063 ms | 0.8934 ms |  3.133 ms |         - |    735 KB |

     */

    [MemoryDiagnoser]
    public class ProjectionBenchmarks : EntityFrameworkBenchMarkBase
    {

        [Benchmark]
        public void NoProjectionAndTracking()
        {
            using var dbContext = CreateContext();

            var orders = dbContext.SettlementOrders.Take(1000).ToList();
        }

        [Benchmark]
        public void NoProjectionAndNoTracking()
        {
            using var dbContext = CreateContext();

            var orders = dbContext.SettlementOrders.AsNoTracking().Take(1000).ToList();
        }

        [Benchmark]
        public void WithProjectionAndTracking()
        {
            using var dbContext = CreateContext();

            var orders = dbContext.SettlementOrders.Select(o => new { o.Identifier, o.PropertyAddress.Street }).Take(1000).ToList();
        }

        [Benchmark]
        public void WithProjectionAndNoTracking()
        {
            using var dbContext = CreateContext();

            var orders = dbContext.SettlementOrders.AsNoTracking().Select(o => new { o.Identifier, o.PropertyAddress.Street }).Take(1000).ToList();
        }

    }
}
