using UltraGroup.Core.Application.Requests.Bookings;

namespace UltraGroup.Core.Application.Interfaces
{
    public interface IBookingService
    {
        Task<IResult> CreateAsync(CreateBookingRequest data);
        Task<IResult> GetBookingsByHotel(int data);
        Task<IResult> GetBookingsDetailsl(int data);
    }
}