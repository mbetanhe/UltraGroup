using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class CreateRoomCommand : IRequest<IResult<RoomEntity>>
    {
        public RoomEntity data;
    }

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, IResult<RoomEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public CreateRoomCommandHandler(IApplicationDbContext context, ILogger<CreateRoomCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<RoomEntity>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = _context.Rooms.Add(request.data);
                await _context.SaveChanges();
                _context.Entry(request.data).GetDatabaseValues();
                return Result<RoomEntity>.Success(request.data, "The room has been created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<RoomEntity>.Fail("An error has ocurred creating room. See message for details");
            }
        }
    }
}
