using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Rooms;
using UltraGroup.Core.Application.Responses;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class EnableDisableRoomCommand : IRequest<IResult<bool>>
    {
        public EnableDisableRoomRequest data { get; set; }

        public class EnableDisableRoomCommandHandler : IRequestHandler<EnableDisableRoomCommand, IResult<bool>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public EnableDisableRoomCommandHandler(IApplicationDbContext context, ILogger<EnableDisableRoomCommandHandler> logger) => (_context, _logger) = (context, logger);

            public async Task<IResult<bool>> Handle(EnableDisableRoomCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _context.Rooms.FindAsync(request.data.RoomId);
                    entity.Room_IsAvailable = request.data.Enabled;

                    _context.Rooms.Update(entity);
                    await _context.SaveChanges();

                    return Result<bool>.Success(request.data.Enabled, request.data.Enabled == true ? "Room is enabled" : "Room is disabled");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                    return Result<bool>.Fail("An error has ocurred updating hotel state. See message for details");
                }
            }
        }
    }
}
