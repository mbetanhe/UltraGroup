using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Hotel;
using UltraGroup.Core.Application.Responses.Hotel;
using UltraGroup.Core.Application.Responses.Room;

namespace UltraGroup.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService) => _hotelService = hotelService;

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<GetHotelResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<GetHotelResponse>))]
        public async Task<IActionResult> GetRoomAsync(int id)
        {
            var result = await _hotelService.GetAsync(id);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<CreateUpdateHotelResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<CreateUpdateHotelResponse>))]
        public async Task<IActionResult> CreateHotelAsync(CreateUpdateHotelRequest request)
        {
            var result = await _hotelService.CreateAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("SetRoom")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<CreateUpdateHotelResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<CreateUpdateHotelResponse>))]
        public async Task<IActionResult> SetRoomRequestAsync(SetRoomRequest request)
        {
            var result = await _hotelService.SetRoomAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Enable")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<bool>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<bool>))]
        public async Task<IActionResult> EnableOrDisableRequestAsync(EnableDisableHotelRequest request)
        {
            var result = await _hotelService.EnableOrDisableAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<CreateUpdateRoomResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<CreateUpdateRoomResponse>))]
        public async Task<IActionResult> UpdateHotelAsync(CreateUpdateHotelRequest request)
        {
            var result = await _hotelService.UpdateAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

    }
}


