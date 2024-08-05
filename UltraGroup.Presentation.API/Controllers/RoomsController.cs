using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroup.Core.Application.Filters;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Rooms;
using UltraGroup.Core.Application.Responses.Room;

namespace UltraGroup.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService) => _roomService = roomService;

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<GetRoomResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<GetRoomResponse>))]
        public async Task<IActionResult> GetRoomAsync(int id)
        {
            var result = await _roomService.GetAsync(id);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<GetRoomResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<IEnumerable<GetRoomResponse>>))]
        public async Task<IActionResult> GetRoomListAsync()
        {
            var result = await _roomService.GetListAsync();

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetReservedOrNot")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<GetRoomResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<IEnumerable<GetRoomResponse>>))]
        public async Task<IActionResult> GetRoomListAsync(bool request)
        {
            var result = await _roomService.GetReservedOrNotAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<CreateUpdateRoomResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<CreateUpdateRoomResponse>))]
        public async Task<IActionResult> CreateRoomAsync(CreateUpdateRoomRequest request)
        {
            var result = await _roomService.CreateAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<CreateUpdateRoomResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<CreateUpdateRoomResponse>))]
        public async Task<IActionResult> UpdateRoomAsync(CreateUpdateRoomRequest request)
        {
            var result = await _roomService.UpdateAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Enable")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<bool>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<bool>))]
        public async Task<IActionResult> EnableOrDisableRequestAsync(EnableDisableRoomRequest request)
        {
            var result = await _roomService.EnableOrDisableAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPost("GetAvailableRooms")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<List<GetRoomResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<List<GetRoomResponse>>))]
        public async Task<IActionResult> GetAvailableRoomsAsync(HotelFilter request)
        {
            var result = await _roomService.GetAvailableRooms(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
