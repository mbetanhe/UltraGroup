using MediatR;
using Microsoft.Extensions.Logging;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Core.Application.CQRS.Commands
{
    public class UpdateRoomCommand : IRequest<IResult<RoomEntity>>
    {
        public RoomEntity data { get; set; }
    }

    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, IResult<RoomEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public UpdateRoomCommandHandler(IApplicationDbContext context, ILogger<UpdateRoomCommandHandler> logger) => (_context, _logger) = (context, logger);

        public async Task<IResult<RoomEntity>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateEntity = _context.Rooms.Find(request.data.ID);

                updateEntity.Room_Descripcion = request.data.Room_Descripcion;
                updateEntity.Room_Price = request.data.Room_Price;
                updateEntity.Room_Tax = request.data.Room_Tax;
                updateEntity.Room_Location = request.data.Room_Location;
                updateEntity.IsReserved = request.data.IsReserved;
                updateEntity.Room_IsAvailable = request.data.Room_IsAvailable;
                updateEntity.Room_Descripcion = request.data.Room_Descripcion;
                updateEntity.TpRoom_ID = request.data.TpRoom_ID;
                updateEntity.Hotel_ID = request.data.Hotel_ID;

                var res = _context.Rooms.Update(updateEntity);
                await _context.SaveChanges();
                _context.Entry(request.data).GetDatabaseValues();

                return Result<RoomEntity>.Success(updateEntity, "The room has been updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                return Result<RoomEntity>.Fail("An error has ocurred creating room. See message for details");
            }
        }
    }
}
