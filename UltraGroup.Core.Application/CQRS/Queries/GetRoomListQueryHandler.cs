using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetRoomListQuery : IRequest<IResult<List<RoomEntity>>>{ }

    public class GetRoomListQueryHandler : IRequestHandler<GetRoomListQuery, IResult<List<RoomEntity>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;
        public GetRoomListQueryHandler(IApplicationDbContext context, ILogger<GetRoomListQueryHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<List<RoomEntity>>> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _context.Rooms
                                    .Include(e => e.Hotel)
                                    .Include(e => e.TP_Room)
                                    .ToList();

                //result.ForEach(e =>
                //{
                //    e.Booking = _context.Bookings.FirstOrDefault(x => x.RoomId == e.ID);
                //    e.Booking.ClientEntity = _context.Clients.Where(x => x.BookingId == e.Booking.ID).ToList();
                //});

                foreach (var room in result)
                {
                    room.Booking = _context.Bookings.FirstOrDefault(x => x.RoomId == room.ID) ?? null;

                    if (room.Booking != null)
                        room.Booking.ClientEntity = _context.Clients.Where(x => x.BookingId == room.Booking.ID)?.ToList();

                }

                return Result<List<RoomEntity>>.Success(result, "List of rooms");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<List<RoomEntity>>.Fail("An error has ocurred getting the rooms. See message for details");
            }
        }
    }
}
