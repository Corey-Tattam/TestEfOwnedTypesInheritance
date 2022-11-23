using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TestEfOwnedTypesInheritance.Infrastructure.Persistence
{
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
}
