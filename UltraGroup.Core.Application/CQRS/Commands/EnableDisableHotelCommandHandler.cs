using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Hotel;
using UltraGroup.Core.Application.Responses;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class EnableDisableHotelCommand : IRequest<IResult<bool>>
    {
        public EnableDisableHotelRequest data { get; set; }
    }

    public class EnableDisableHotelCommandHandler : IRequestHandler<EnableDisableHotelCommand, IResult<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public EnableDisableHotelCommandHandler(IApplicationDbContext context, ILogger<EnableDisableHotelCommand> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<bool>> Handle(EnableDisableHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Hotels.FindAsync(request.data.HotelId);
                entity.Htl_IsAvailable = request.data.Enabled;

                _context.Hotels.Update(entity);
                await _context.SaveChanges();

                return Result<bool>.Success(request.data.Enabled, request.data.Enabled == true ? "Hotel is enabled" : "Hotel is disabled");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<bool>.Fail("An error has ocurred updating hotel state. See message for details");
            }
        }
    }
}
