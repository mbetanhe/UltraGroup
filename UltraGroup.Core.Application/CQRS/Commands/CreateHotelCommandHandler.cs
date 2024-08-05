using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class CreateHotelCommand : IRequest<IResult<HotelEntity>>
    {
        public string UserId;
        public HotelEntity data;
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, IResult<HotelEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public CreateHotelCommandHandler(IApplicationDbContext context, ILogger<CreateHotelCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<HotelEntity>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = _context.Hotels.Add(request.data);
                await _context.SaveChanges();
                _context.Entry(request.data).GetDatabaseValues();
                return Result<HotelEntity>.Success(request.data, "The hotel has been created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<HotelEntity>.Fail("An error has ocurred creating hotel. See message for details");
            }
        }
    }
}
