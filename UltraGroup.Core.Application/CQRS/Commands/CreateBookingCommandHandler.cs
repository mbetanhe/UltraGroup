using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Application.Responses.Booking;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class CreateBookingCommand : IRequest<IResult<bool>>
    {
        public BookingEntity data { get; set; }
    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, IResult<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;
        public CreateBookingCommandHandler(IApplicationDbContext context, ILogger<CreateBookingCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<bool>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = _context.Bookings.Add(request.data);
                await _context.SaveChanges();

                return Result<bool>.Success(true, "Reservation for room has been created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<bool>.Fail("An error has ocurred creating reservation. See message for details");
            }
        }
    }
}
