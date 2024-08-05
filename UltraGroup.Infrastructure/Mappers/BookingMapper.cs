using AutoMapper;
using System.Globalization;
using UltraGroup.Core.Application.Requests.Bookings;
using UltraGroup.Core.Application.Responses.Booking;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<CreateBookingRequest, BookingEntity>()
               .ForMember(src => src.Booking_FullNameEmergency, opt => opt.MapFrom(dest => dest.FullNameEmergency))
               .ForMember(src => src.Booking_PhoneEmergency, opt => opt.MapFrom(dest => dest.PhoneEmergency))
               .ForMember(src => src.Booking_StartDate, opt => opt.MapFrom<DateStartDateMap>())
               .ForMember(src => src.Booking_EndDate, opt => opt.MapFrom<DateEndDateMap>())
               .ForMember(src => src.ClientEntity, opt => opt.MapFrom(dest => dest.clients))
               .ReverseMap();


            CreateMap<BookingEntity, GetBookingResponse>()
                .ForMember(src => src.RoomdId, opt => opt.MapFrom(dest => dest.RoomId))
                .ForMember(src => src.Clients, opt => opt.MapFrom<CountClientsMap>())
                .ForMember(src => src.Stardate, opt => opt.MapFrom<ToDateStartDateMap>())
                .ForMember(src => src.EndDate, opt => opt.MapFrom<ToDateEndtDateMap>());

            CreateMap<BookingEntity, GetBookingDetailResponse>()
                .ForMember(src => src.BookingId, opt => opt.MapFrom(dest => dest.ID))
                .ForMember(src => src.Stardate, opt => opt.MapFrom<ToDateStartDateMapRes>())
                .ForMember(src => src.EndDate, opt => opt.MapFrom<ToDateEndtDateMapRes>())
                .ForMember(src => src.FullNameEmergency, opt => opt.MapFrom(dest => dest.Booking_FullNameEmergency))
                .ForMember(src => src.PhoneEmergency, opt => opt.MapFrom(dest => dest.Booking_PhoneEmergency))
                .ForMember(src => src.Clients, opt => opt.MapFrom(dest => dest.ClientEntity))
                .ForMember(src => src.Room, opt => opt.MapFrom(dest => dest.RoomEntity))
                .ForMember(src => src.Total, opt => opt.MapFrom<TotalMap>());
        }

        internal class DateStartDateMap : IValueResolver<CreateBookingRequest, BookingEntity, DateTime>
        {
            public DateTime Resolve(CreateBookingRequest source, BookingEntity destination, DateTime destMember, ResolutionContext context)
            {
                DateTime startDate = DateTime.Now;

                try
                {
                    var res = DateTime.TryParseExact(source.Stardate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                    return startDate;
                }
                catch (Exception)
                {
                    return startDate;
                }
            }
        }

        internal class ToDateStartDateMap : IValueResolver<BookingEntity, GetBookingResponse, string>
        {
            public string Resolve(BookingEntity source, GetBookingResponse destination, string destMember, ResolutionContext context)
            {
                return source.Booking_StartDate.ToString("yyyy-MM-dd");
            }
        }

        internal class ToDateEndtDateMap : IValueResolver<BookingEntity, GetBookingResponse, string>
        {
            public string Resolve(BookingEntity source, GetBookingResponse destination, string destMember, ResolutionContext context)
            {
                return source.Booking_EndDate.ToString("yyyy-MM-dd");
            }
        }

        internal class ToDateStartDateMapRes : IValueResolver<BookingEntity, GetBookingDetailResponse, string>
        {
            public string Resolve(BookingEntity source, GetBookingDetailResponse destination, string destMember, ResolutionContext context)
            {
                return source.Booking_StartDate.ToString("yyyy-MM-dd");
            }
        }

        internal class ToDateEndtDateMapRes : IValueResolver<BookingEntity, GetBookingDetailResponse, string>
        {
            public string Resolve(BookingEntity source, GetBookingDetailResponse destination, string destMember, ResolutionContext context)
            {
                return source.Booking_EndDate.ToString("yyyy-MM-dd");
            }
        }

        internal class DateEndDateMap : IValueResolver<CreateBookingRequest, BookingEntity, DateTime>
        {
            public DateTime Resolve(CreateBookingRequest source, BookingEntity destination, DateTime destMember, ResolutionContext context)
            {
                DateTime endDate = DateTime.Now;

                try
                {
                    var res = DateTime.TryParseExact(source.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

                    return endDate;
                }
                catch (Exception)
                {
                    return endDate;
                }
            }
        }

        internal class CountClientsMap : IValueResolver<BookingEntity, GetBookingResponse, int>
        {
            public int Resolve(BookingEntity source, GetBookingResponse destination, int destMember, ResolutionContext context)
            {
                return source.ClientEntity != null ? source.ClientEntity.Count : 0;
            }
        }

        internal class TotalMap : IValueResolver<BookingEntity, GetBookingDetailResponse, decimal>
        {
            public decimal Resolve(BookingEntity source, GetBookingDetailResponse destination, decimal destMember, ResolutionContext context)
            {
                return (source.RoomEntity.Room_Price + source.RoomEntity.Room_Tax) * source.ClientEntity.Count();
            }
        }
    }
}
