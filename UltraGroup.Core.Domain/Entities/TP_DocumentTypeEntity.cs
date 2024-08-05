using System.Collections.ObjectModel;

namespace UltraGroup.Core.Domain.Entities
{
    public class TP_DocumentTypeEntity : BaseEntity
    {
        public string Description { get; set; }

        public Collection<ClientEntity> Clients { get; set; }
    }
}
