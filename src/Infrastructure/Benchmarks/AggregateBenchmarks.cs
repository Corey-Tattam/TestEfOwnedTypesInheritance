using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Features.Benchmarks
{
    /*
        // * Summary *

        BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
        Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
        .NET SDK=5.0.401
          [Host]     : .NET 5.0.10 (5.0.1021.41214), X64 RyuJIT
          DefaultJob : .NET 5.0.10 (5.0.1021.41214), X64 RyuJIT


        |                                          Method |      Mean |     Error |    StdDev |    Median |     Gen 0 | Allocated |
        |------------------------------------------------ |----------:|----------:|----------:|----------:|----------:|----------:|
        |   ClientSideEvaluationNoProjectionAndNoTracking | 26.627 ms | 1.2322 ms | 3.5945 ms | 27.206 ms | 1000.0000 |  6,994 KB |
        | ClientSideEvaluationWithProjectionAndNoTracking |  5.235 ms | 0.4093 ms | 1.1743 ms |  4.848 ms |         - |  1,832 KB |
        |                  ServerSideEvaluationNoTracking |  3.667 ms | 0.2450 ms | 0.7148 ms |  3.504 ms |         - |     43 KB |

     */
    [MemoryDiagnoser]
    public class AggregateBenchmarks : EntityFrameworkBenchMarkBase
    {
        private const int MaxResultsToTake = 1000;

        [Benchmark]
        public void ClientSideEvaluationNoProjectionAndNoTracking()
        {
            using var dbContext = CreateContext();

            var documents = dbContext.Documents
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Take(MaxResultsToTake)
                .ToList();

            var averageDocumentsPerOrder = documents.GroupBy(d => d.OrderId)
                .Select(grp => grp.Count())
                .Average();

            //Console.WriteLine($"Average Documents per Order: {averageDocumentsPerOrder}");
        }

        [Benchmark]
        public void ClientSideEvaluationWithProjectionAndNoTracking()
        {
            using var dbContext = CreateContext();

            var documents = dbContext.Documents
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Select(d => d.OrderId)
                .Take(MaxResultsToTake)
                .ToList();

            var averageDocumentsPerOrder = documents.GroupBy(OrderId => OrderId)
                .Select(grp => grp.Count())
                .Average();

            //Console.WriteLine($"Average Documents per Order: {averageDocumentsPerOrder}");
        }

        [Benchmark]
        public void ServerSideEvaluationNoTracking()
        {
            using var dbContext = CreateContext();

            var averageDocumentsPerOrder = dbContext.Documents
                .AsNoTracking()
                .Take(MaxResultsToTake)
                .Where(d => !d.IsDeleted)
                .GroupBy(d => d.OrderId)
                .Select(grp => grp.Count())
                .Average();

            //Console.WriteLine($"Average Documents per Order: {averageDocumentsPerOrder}");
        }
        
    }
}
