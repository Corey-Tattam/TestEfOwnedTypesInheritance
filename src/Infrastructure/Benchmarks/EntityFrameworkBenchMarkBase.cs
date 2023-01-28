using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;

namespace Application.Features.Benchmarks
{
    public abstract class EntityFrameworkBenchMarkBase
    {
        protected static TestDbContext CreateContext() =>
            new TestDbContext(
                new DbContextOptionsBuilder<TestDbContext>()
                    .UseSqlServer("Server=.\\SQLEXPRESS;Database=TestEfOwnedInheritance;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options, 
                Enumerable.Empty<IInterceptor>());
    }
}
