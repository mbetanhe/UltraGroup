using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class UpdateHotelCommand : IRequest<IResult<HotelEntity>>
    {
        public HotelEntity data { get; set; }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, IResult<HotelEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public UpdateHotelCommandHandler(IApplicationDbContext context, ILogger<UpdateHotelCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<HotelEntity>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateEntity = _context.Hotels.Find(request.data.ID);

                updateEntity.Htl_Description = request.data.Htl_Description;
                updateEntity.Htl_Name = request.data.Htl_Name;
                updateEntity.Htl_IsAvailable = request.data.Htl_IsAvailable;
                updateEntity.Htl_Address = request.data.Htl_Address;
                updateEntity.Htl_UsersQuantity = request.data.Htl_UsersQuantity;

                var res = _context.Hotels.Update(updateEntity);
                await _context.SaveChanges();
                _context.Entry(request.data).GetDatabaseValues();

                return Result<HotelEntity>.Success(updateEntity, "The room has been updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<HotelEntity>.Fail("An error has ocurred updating hotel. See message for details");
            }
        }
    }
}
