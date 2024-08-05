using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Configurations
{
    public class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> entity)
        {
            entity.ToTable("Clients");
            entity.HasOne(e => e.TPDocumentType).WithMany().HasForeignKey(fk => fk.TypeDocumentId).IsRequired(false);
        }
    }
}
