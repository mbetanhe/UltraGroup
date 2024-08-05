using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Application.Responses.Booking;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetBookingQuery : IRequest<IResult<BookingEntity>>
    {
        public int BookingId { get; set; }  
    }

    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, IResult<BookingEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;
        public GetBookingQueryHandler(IApplicationDbContext context, ILogger<GetBookingQueryHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<BookingEntity>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _context.Bookings
                                    .Include(e => e.ClientEntity)
                                    .FirstOrDefault(e => e.ID == request.BookingId);

                if (result != null)
                {
                        result.RoomEntity = _context.Rooms.Include(e => e.Hotel).Include(e => e.TP_Room).FirstOrDefault(e => e.ID == result.RoomId);
                }

                if (result == null)
                    return Result<BookingEntity>.Fail("The reservation does not exists");

                return Result<BookingEntity>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<BookingEntity>.Fail("An error has ocurred getting the room. See message for details");
            }
        }
    }
}
