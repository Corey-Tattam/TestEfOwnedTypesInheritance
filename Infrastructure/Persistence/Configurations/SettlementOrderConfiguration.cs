using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TestEfOwnedTypesInheritance.Infrastructure.Persistence
{
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
}
