using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TestEfOwnedTypesInheritance.Infrastructure.Persistence
{
    public class IndividualConsumerConfiguration : IEntityTypeConfiguration<IndividualConsumer>
    {

        public void Configure(EntityTypeBuilder<IndividualConsumer> entity)
        {
            entity.HasBaseType<ConsumerBase>();
        }
    }
}
