using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetBookingsByHotelQuery : IRequest<IResult<List<BookingEntity>>>
    {
        public int HotelId { get; set; }
    }

    public class GetBookingsByHotelQueryHandler : IRequestHandler<GetBookingsByHotelQuery, IResult<List<BookingEntity>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public GetBookingsByHotelQueryHandler(IApplicationDbContext context, ILogger<GetBookingsByHotelQueryHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<List<BookingEntity>>> Handle(GetBookingsByHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<BookingEntity> bookings = new();

                //Get rooms with reservations
                var rooms = _context.Rooms.Where(e => e.Hotel_ID == request.HotelId && e.IsReserved == true);
                if (rooms.Any())
                {
                    foreach (var room in rooms)
                    {
                        room.Booking = _context.Bookings.FirstOrDefault(e => e.RoomId == room.ID);
                        room.Booking.ClientEntity = _context.Clients.Where(x => x.BookingId == room.Booking.ID).ToList();
                        bookings.Add(room.Booking);
                    }
                }

                if (rooms == null || bookings.Count == 0)
                    return Result<List<BookingEntity>>.Fail("Does not exists reservations for hotel");

                return Result<List<BookingEntity>>.Success(bookings, "List of reservations");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<List<BookingEntity>>.Fail("An error has ocurred getting the hotel. See message for details");
            }
        }
    }
}
