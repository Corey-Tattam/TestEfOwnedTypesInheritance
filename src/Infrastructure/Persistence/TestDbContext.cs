using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;

namespace Infrastructure.Persistence
{

    public class TestDbContext : DbContext, ITestDbContext
    {
        private readonly IEnumerable<IInterceptor> _interceptors;

        public TestDbContext(DbContextOptions<TestDbContext> options, IEnumerable<IInterceptor> interceptors)
            : base(options)
        {
            _interceptors = interceptors;
        }

        public DbSet<SettlementOrder> SettlementOrders { get; set; } = null!;

        public DbSet<OrganisationalConsumer> OrganisationalConsumers { get; set; } = null!;

        public DbSet<IndividualConsumer> IndividualConsumers { get; set; } = null!;

        public DbSet<Document> Documents { get; set ; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptors);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
