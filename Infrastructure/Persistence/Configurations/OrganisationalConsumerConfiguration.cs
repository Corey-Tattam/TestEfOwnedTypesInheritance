using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TestEfOwnedTypesInheritance.Infrastructure.Persistence
{
    public class OrganisationalConsumerConfiguration : IEntityTypeConfiguration<OrganisationalConsumer>
    {

        public void Configure(EntityTypeBuilder<OrganisationalConsumer> entity)
        {
            entity.HasBaseType<ConsumerBase>();
            entity.Property(e => e.CompanyType).HasColumnType("char(20)");
        }
    }
}
