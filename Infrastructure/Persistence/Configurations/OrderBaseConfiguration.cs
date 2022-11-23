using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TestEfOwnedTypesInheritance.Infrastructure.Persistence
{
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
}
