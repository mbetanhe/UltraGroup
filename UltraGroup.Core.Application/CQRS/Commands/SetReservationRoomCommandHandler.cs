using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class SetReservationRoomCommand : IRequest<IResult<bool>>
    {
        public int RoomId { get; set; }
    }

    public class SetReservationRoomCommandHandler : IRequestHandler<SetReservationRoomCommand, IResult<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public SetReservationRoomCommandHandler(IApplicationDbContext context, ILogger<SetReservationRoomCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<bool>> Handle(SetReservationRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = _context.Rooms.Find(request.RoomId);
                res.IsReserved = true;
                _context.Rooms.Update(res);
                await _context.SaveChanges();

                return Result<bool>.Success(true, "The room reservation was created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<bool>.Fail("An error has ocurred reservating the room. See message for details");
            }
        }
    }
}
