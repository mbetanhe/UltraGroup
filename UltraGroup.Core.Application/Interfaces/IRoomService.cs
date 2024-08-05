using UltraGroup.Core.Application.Filters;
using UltraGroup.Core.Application.Requests.Rooms;
using UltraGroup.Core.Application.Responses.Room;

namespace UltraGroup.Core.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IResult<GetRoomResponse>> GetAsync(int id);
        Task<IResult<List<GetRoomResponse>>> GetListAsync();
        Task<IResult<List<GetRoomResponse>>> GetReservedOrNotAsync(bool data);
        Task<IResult> CreateAsync(CreateUpdateRoomRequest data);
        Task<IResult> UpdateAsync(CreateUpdateRoomRequest data);
        Task<IResult> EnableOrDisableAsync(EnableDisableRoomRequest data);
        Task<IResult> SetReveationtoRoomAsync(int id);
        Task<IResult> GetAvailableRooms(HotelFilter data);

    }
}