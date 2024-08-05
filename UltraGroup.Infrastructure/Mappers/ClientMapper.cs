using AutoMapper;
using UltraGroup.Core.Application.Requests.Client;
using UltraGroup.Core.Application.Responses.Client;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<ClientEntity, CreateClientRequest>()
            .ForMember(src => src.FullName, opt => opt.MapFrom(dest => dest.Client_Fullname))
            .ForMember(src => src.Email, opt => opt.MapFrom(dest => dest.Client_Email))
            .ForMember(src => src.Document, opt => opt.MapFrom(dest => dest.Client_Document))
            .ForMember(src => src.Gender, opt => opt.MapFrom(dest => dest.Client_Gender))
            .ForMember(src => src.BirthDate, opt => opt.MapFrom(dest => dest.Client_Birthdate))
            .ForMember(src => src.Phone, opt => opt.MapFrom(dest => dest.Client_ContactPhone))
            .ForMember(src => src.DocumentTypeId, opt => opt.MapFrom(dest => dest.TypeDocumentId))
            .ReverseMap();

            CreateMap<ClientEntity, GetClientResponse>()
            .ForMember(src => src.FullName, opt => opt.MapFrom(dest => dest.Client_Fullname))
                .ForMember(src => src.Email, opt => opt.MapFrom(dest => dest.Client_Email))
                .ForMember(src => src.Document, opt => opt.MapFrom(dest => dest.Client_Document))
                .ForMember(src => src.Gender, opt => opt.MapFrom(dest => dest.Client_Gender))
                .ForMember(src => src.BirthDate, opt => opt.MapFrom(dest => dest.Client_Birthdate))
                .ForMember(src => src.Phone, opt => opt.MapFrom(dest => dest.Client_ContactPhone))
                .ForMember(src => src.documentType, opt => opt.MapFrom(dest => dest.TPDocumentType))
                .ReverseMap();
        }
    }
}
