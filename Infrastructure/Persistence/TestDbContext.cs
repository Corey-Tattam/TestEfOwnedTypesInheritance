using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{

    public class TestDbContext : DbContext, ITestDbContext
    {

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public DbSet<SettlementOrder> SettlementOrders { get; set; } = null!;

        public DbSet<OrganisationalConsumer> OrganisationalConsumers { get; set; } = null!;

        public DbSet<IndividualConsumer> IndividualConsumers { get; set; } = null!;

        public DbSet<Document> Documents { get; set ; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        }

    }
}
