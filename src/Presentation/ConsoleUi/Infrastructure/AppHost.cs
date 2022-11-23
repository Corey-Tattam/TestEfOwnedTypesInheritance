using Application.Features.Benchmarks;
using Application.Interfaces.Demos;
using BenchmarkDotNet.Running;
using ConsoleUi.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUi.Infrastructure
{
    public class AppHost : IAppHost
    {
        private readonly IServiceProvider _serviceProvider;

        public AppHost(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<int> RunAsync(string[] args) 
        {
            // Demos
            var demoRunner = _serviceProvider.GetRequiredService<IDemoCollectionRunner>();
            await demoRunner.RunAllAsync(CancellationToken.None);

            // Benchmarking
#if !DEBUG
            var summary = BenchmarkRunner.Run<ProjectionBenchmarks>();
            var aggSummary = BenchmarkRunner.Run<AggregateBenchmarks>();
#endif
            return 0;
        }
    }
}
