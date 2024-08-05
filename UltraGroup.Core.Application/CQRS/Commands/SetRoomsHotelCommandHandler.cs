using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Hotel;
using UltraGroup.Core.Application.Responses;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class SetRoomsHotelCommand : IRequest<IResult<bool>>
    {
        public SetRoomRequest data;
    }

    public class SetRoomsHotelCommandHandler : IRequestHandler<SetRoomsHotelCommand, IResult<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public SetRoomsHotelCommandHandler(IApplicationDbContext context, ILogger<SetRoomsHotelCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<bool>> Handle(SetRoomsHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _context.Rooms.Find(request.data.RoomId);

                if(result.IsReserved)
                    return Result<bool>.Success(true, "The room to be assigned is reserved\r\n");

                if(request.data.RoomIds != null && request.data.RoomIds.Count > 0)
                {
                    request.data.RoomIds.ForEach(async e =>
                    {
                        var room = await _context.Rooms.FindAsync(e);
                        if (room != null) {
                            room.Hotel_ID = request.data.HotelId;
                            _context.Rooms.Update(result);
                        }
                    });

                    _context.SaveChanges();
                    return Result<bool>.Success(true, "The rooms has been asigned to hotel");
                }

                    result.Hotel_ID = request.data.HotelId;

                    _context.Rooms.Update(result);

                _context.SaveChanges();

                return Result<bool>.Success(true, "The room has been asigned to hotel");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<bool>.Fail("An error has ocurred setting the room. See message for details");
            }
        }
    }
}
