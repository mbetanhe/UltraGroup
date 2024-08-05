using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetReservedOrNotRoomsQuery : IRequest<IResult<List<RoomEntity>>>
    {
        public bool IsReserved { get; set; }
    }

    public class GetReservedRoomsQueryHandler : IRequestHandler<GetReservedOrNotRoomsQuery, IResult<List<RoomEntity>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;
        public GetReservedRoomsQueryHandler(IApplicationDbContext context, ILogger<GetReservedRoomsQueryHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<List<RoomEntity>>> Handle(GetReservedOrNotRoomsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _context.Rooms
                                    .Include(e => e.Hotel)
                                    .Include(e => e.TP_Room)
                                    .Where(e => e.IsReserved == request.IsReserved)
                                    .ToList();

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
