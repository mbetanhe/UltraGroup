using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Configurations
{
    public class BookingEntityConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> entity)
        {
            entity.ToTable("Bookings");
            entity.HasKey(e => e.ID);

            entity.HasMany(e => e.ClientEntity)
                .WithOne(e => e.Booking)
                .HasForeignKey(e => e.BookingId);
        }
    }
}
