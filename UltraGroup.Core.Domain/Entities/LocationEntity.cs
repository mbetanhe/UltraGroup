using System.Collections.ObjectModel;

namespace UltraGroup.Core.Domain.Entities
{
    public class LocationEntity : BaseEntity
    {
        public string Description { get; set; }

        public Collection<HotelEntity> Hotels { get; set; }
    }
}
