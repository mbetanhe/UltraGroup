using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroup.Core.Application.CQRS.Commands;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetRoomQuery : IRequest<IResult<RoomEntity>>
    {
        public int roomId { get; set; }
    }

    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, IResult<RoomEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public GetRoomQueryHandler(IApplicationDbContext context, ILogger<GetRoomQueryHandler> logger) => (_context, _logger) = (context, logger);
        public async Task<IResult<RoomEntity>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _context.Rooms
                                    .Include(e => e.Hotel)
                                    .Include(e => e.TP_Room)
                                    .FirstOrDefault(e => e.ID == request.roomId);

                result.Booking = _context.Bookings.FirstOrDefault(e => e.RoomId == result.ID) ?? null;

                if (result.Booking != null)
                    result.Booking.ClientEntity = _context.Clients.Where(x => x.BookingId == result.Booking.ID)?.ToList();

                if (result == null)
                    return Result<RoomEntity>.Fail("The room does not exists");

                return Result<RoomEntity>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<RoomEntity>.Fail("An error has ocurred getting the room. See message for details");
            }
        }
    }
}
