using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Bookings;
using UltraGroup.Core.Application.Responses.Booking;

namespace UltraGroup.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService) => _bookingService = bookingService;

        [HttpPost("Create")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<bool>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<bool>))]
        public async Task<IActionResult> CreateAsync(CreateBookingRequest request)
        {
            var result = await _bookingService.CreateAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{HotelId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<GetBookingResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<IEnumerable<GetBookingResponse>>))]
        public async Task<IActionResult> GetBookingsByHotelAsync(int HotelId)
        {
            var result = await _bookingService.GetBookingsByHotel(HotelId);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetBookingDetails")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<GetBookingDetailResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<GetBookingDetailResponse>))]
        public async Task<IActionResult> GetBookingDetailsAsync(int BookingId)
        {
            var result = await _bookingService.GetBookingsDetailsl(BookingId);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
