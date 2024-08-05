using System;

namespace UltraGroup.Core.Domain.Entities
{
    public class ClientEntity : BaseEntity
    {
        public string Client_Fullname { get; set; }
        public DateTime Client_Birthdate { get; set; }
        public string Client_Document { get; set; }
        public string Client_Gender { get; set; }
        public string Client_Email { get; set; }
        public string Client_ContactPhone { get; set; }
        public int TypeDocumentId { get; set; }
        public int BookingId { get; set; }  

        public BookingEntity Booking { get; set; }
        public TP_DocumentTypeEntity TPDocumentType { get; set; }
    }
}
