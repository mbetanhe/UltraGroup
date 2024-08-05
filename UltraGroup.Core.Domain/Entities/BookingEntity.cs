using System;
using System.Collections.Generic;

namespace UltraGroup.Core.Domain.Entities
{
    public class BookingEntity : BaseEntity
    {
        public DateTime Booking_StartDate { get; set; }
        public DateTime Booking_EndDate { get; set; }
        public string Booking_FullNameEmergency { get; set; }
        public string Booking_PhoneEmergency { get; set; }
        public int RoomId { get; set; }

        public RoomEntity RoomEntity { get; set; }

        public ICollection<ClientEntity> ClientEntity { get; set; }  

    }
}
