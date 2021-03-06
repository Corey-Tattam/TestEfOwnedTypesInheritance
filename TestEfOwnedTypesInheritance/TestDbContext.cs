﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestEfOwnedTypesInheritance.Entities;

namespace TestEfOwnedTypesInheritance
{
    public interface ITestDbContext : IDisposable
    {
        DbSet<IndividualConsumer> IndividualConsumers { get; set; }
        DbSet<OrganisationalConsumer> OrganisationalConsumers { get; set; }
        DbSet<SettlementOrder> SettlementOrders { get; set; }
        DbSet<Document> Documents { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

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

    public class OrderBaseConfiguration : IEntityTypeConfiguration<OrderBase>
    {

        public void Configure(EntityTypeBuilder<OrderBase> entity)
        {
            entity.ToTable("Orders");

            entity.HasDiscriminator(o => o.OrderType)
                .HasValue<SettlementOrder>(OrderType.SettlementOrder)
                .HasValue<CaveatOrder>(OrderType.CaveatOrder);

            entity.OwnsOne(o => o.PropertyAddress);
        }
    }

    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {

        public void Configure(EntityTypeBuilder<Document> entity)
        {
            entity.ToTable("Documents");
        }
    }

    public class SettlementOrderConfiguration : IEntityTypeConfiguration<SettlementOrder>
    {

        public void Configure(EntityTypeBuilder<SettlementOrder> entity)
        {
            entity.HasBaseType<OrderBase>();

            entity.OwnsOne(o => o.ForwardingAddress, fa => 
            {
                fa.Property(e => e.State).HasColumnName("ForwardingAddressState");
                fa.Property(e => e.Street).HasColumnName("ForwardingAddressStreet");
                fa.Property(e => e.Suburb).HasColumnName("ForwardingAddressSuburb");
                fa.Property(e => e.Country).HasColumnName("ForwardingAddressCountry");
            });
        }
    }

    public class ConsumerBaseConfiguration : IEntityTypeConfiguration<ConsumerBase>
    {

        public void Configure(EntityTypeBuilder<ConsumerBase> entity)
        {
            entity.ToTable("Consumers");

            entity.HasDiscriminator<bool>("IsOrganisation")
                .HasValue<IndividualConsumer>(false)
                .HasValue<OrganisationalConsumer>(true);

            entity.OwnsOne(o => o.Address, a => 
            {
                a.Property(e => e.State).HasColumnName("State");
                a.Property(e => e.Street).HasColumnName("Street");
                a.Property(e => e.Suburb).HasColumnName("Suburb");
            });
        }
    }

    public class IndividualConsumerConfiguration : IEntityTypeConfiguration<IndividualConsumer>
    {

        public void Configure(EntityTypeBuilder<IndividualConsumer> entity)
        {
            entity.HasBaseType<ConsumerBase>();
        }
    }

    public class OrganisationalConsumerConfiguration : IEntityTypeConfiguration<OrganisationalConsumer>
    {

        public void Configure(EntityTypeBuilder<OrganisationalConsumer> entity)
        {
            entity.HasBaseType<ConsumerBase>();
            entity.Property(e => e.CompanyType).HasColumnType("char(20)");
        }
    }
}
