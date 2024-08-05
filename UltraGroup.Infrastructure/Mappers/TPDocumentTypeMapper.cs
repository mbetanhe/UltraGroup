using AutoMapper;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class TPDocumentTypeMapper : Profile
    {
        public TPDocumentTypeMapper()
        {
            CreateMap<TP_DocumentTypeEntity, GetDocumentTypeResponse>()
                .ForMember(src => src.Id, opt => opt.MapFrom(dest => dest.ID))
                .ForMember(src => src.name, opt => opt.MapFrom(dest => dest.Description))
                .ReverseMap();
        }
    }
}
