using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;

namespace Infrastructure.Persistence
{
    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TestEfOwnedInheritance;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new TestDbContext(optionsBuilder.Options, Enumerable.Empty<IInterceptor>());
        }
    }
}
