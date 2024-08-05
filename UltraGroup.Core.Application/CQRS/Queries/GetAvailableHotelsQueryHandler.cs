using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Filters;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetAvailableRoomsQuery : IRequest<IResult<List<RoomEntity>>>
    {
        public HotelFilter Filter { get; set; }
    }

    public class GetAvailableHotelsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, IResult<List<RoomEntity>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;
        public GetAvailableHotelsQueryHandler(IApplicationDbContext context, ILogger<GetAvailableHotelsQueryHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<List<RoomEntity>>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roomsAvailables = _context.Rooms
                                                    .Include(e => e.Hotel)
                                                    .Include(e => e.TP_Room)
                                                    .Where(e => e.IsReserved == false && e.Room_IsAvailable == true && e.Hotel.LocationId == request.Filter.LocationId && e.Hotel.Htl_UsersQuantity >= request.Filter.Quantity)
                                                    .ToList();

                if (roomsAvailables == null)
                    return Result<List<RoomEntity>>.Success(roomsAvailables, "There is not room availables");

                return Result<List<RoomEntity>>.Success(roomsAvailables, "Room availables for booking");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<List<RoomEntity>>.Fail("An error has ocurred getting the rooms. See message for details");
            }
        }
    }
}
