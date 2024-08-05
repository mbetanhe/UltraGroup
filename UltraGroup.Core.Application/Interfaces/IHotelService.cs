using UltraGroup.Core.Application.Requests.Hotel;

namespace UltraGroup.Core.Application.Interfaces
{
    public interface IHotelService
    {
        Task<IResult> GetAsync(int id);
        Task<IResult> CreateAsync(CreateUpdateHotelRequest data);
        Task<IResult> UpdateAsync(CreateUpdateHotelRequest data);
        Task<IResult> SetRoomAsync(SetRoomRequest data);
        Task<IResult> EnableOrDisableAsync(EnableDisableHotelRequest data);
    }
}