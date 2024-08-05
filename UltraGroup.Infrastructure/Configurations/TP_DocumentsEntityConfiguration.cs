using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Configurations
{
    public class TP_DocumentsEntityConfiguration : IEntityTypeConfiguration<TP_DocumentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TP_DocumentTypeEntity> entity)
        {
            entity.ToTable("TP_Documents");

            entity.HasKey(e => e.ID);
            entity.HasMany(e => e.Clients).WithOne(e => e.TPDocumentType).HasForeignKey(e => e.TypeDocumentId).IsRequired(false);
        }
    }
}
