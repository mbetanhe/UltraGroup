using System;

namespace UltraGroup.Core.Domain.Entities
{
    public class HotelEntity : BaseEntity
    {
        public string Htl_Name { get; set; }
        public string Htl_Description { get; set; }
        public string Htl_Address { get; set; }
        public int Htl_UsersQuantity { get; set; }
        public bool Htl_IsAvailable { get; set; }
        public string CreatedByUserId { get; set; }

        public int LocationId { get; set; }

        public LocationEntity Location { get; set; }

    }
}
