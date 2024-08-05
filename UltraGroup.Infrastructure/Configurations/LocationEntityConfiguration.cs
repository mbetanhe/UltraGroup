using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Configurations
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<LocationEntity>
    {
        public void Configure(EntityTypeBuilder<LocationEntity> entity)
        {
            entity.ToTable("Locations");

            entity.HasKey(e => e.ID);

            entity.HasMany(e => e.Hotels).WithOne(e => e.Location).HasForeignKey(e => e.LocationId).IsRequired(false);
        }
    }

}
