using System.ComponentModel.DataAnnotations.Schema;

namespace UltraGroup.Core.Domain.Entities
{
    public class RoomEntity : BaseEntity
    {
        public string Room_Descripcion { get; set; }
        public decimal Room_Price { get; set; }
        public decimal Room_Tax { get; set; }
        public bool Room_IsAvailable { get; set; }
        public int TpRoom_ID { get; set; }
        public bool IsReserved { get; set; }
        public string Room_Location { get; set; }   
        public int Hotel_ID { get; set; }
        public int BookingId { get; set; }
        public TP_RoomEntity TP_Room { get; set; }
        public HotelEntity Hotel { get; set; }

        //[NotMapped]
        public virtual BookingEntity Booking { get; set; }
    }
}
