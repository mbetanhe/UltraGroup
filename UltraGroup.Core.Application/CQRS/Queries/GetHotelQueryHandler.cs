using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Queries
{
    public class GetHotelQuery : IRequest<IResult<HotelEntity>>
    {
        public int HotelId { get; set; }
    }

    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, IResult<HotelEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public GetHotelQueryHandler(IApplicationDbContext context, ILogger<GetHotelQueryHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<HotelEntity>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _context.Hotels
                    .Include(e => e.Location)
                                    .FirstOrDefault(e => e.ID == request.HotelId);

                if (result == null)
                    return Result<HotelEntity>.Fail("The hotel does not exists");

                return Result<HotelEntity>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<HotelEntity>.Fail("An error has ocurred getting the hotel. See message for details");
            }
        }
    }
}
