using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<HotelEntity> Hotels { get; set; }
        DbSet<BookingEntity> Bookings { get; set; }
        DbSet<ClientEntity> Clients { get; set; }
        DbSet<RoomEntity> Rooms { get; set; }
        DbSet<TP_RoomEntity> TP_Rooms { get; set; }
        DbSet<TP_DocumentTypeEntity> TP_Documents { get; set; }
        DbSet<LocationEntity> Locations { get; set; }

        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;


        Task<int> SaveChanges();
    }
}
