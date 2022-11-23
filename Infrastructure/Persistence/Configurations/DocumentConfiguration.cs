using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TestEfOwnedTypesInheritance.Infrastructure.Persistence
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {

        public void Configure(EntityTypeBuilder<Document> entity)
        {
            entity.ToTable("Documents");
        }
    }
}
