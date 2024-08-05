using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<HotelEntity> Hotels { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<TP_RoomEntity> TP_Rooms { get; set; }
        public DbSet<TP_DocumentTypeEntity> TP_Documents { get; set; }  
        public DbSet<LocationEntity> Locations { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
