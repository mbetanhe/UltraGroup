using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Configurations
{
    public class RoomEntityConfiguration : IEntityTypeConfiguration<RoomEntity>
    {
        public void Configure(EntityTypeBuilder<RoomEntity> entity)
        {
            entity.ToTable("Rooms");
            entity.HasKey(e => e.ID);

            entity.HasOne(e => e.Hotel).WithMany().HasForeignKey(fk => fk.Hotel_ID).IsRequired(false);
            entity.HasOne(e => e.Booking).WithMany().HasForeignKey(fk => fk.BookingId).IsRequired(false);
            entity.HasOne(e => e.TP_Room).WithMany().HasForeignKey(fk => fk.TpRoom_ID).IsRequired(false);

        }
    }
}
