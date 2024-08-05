using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests;
using UltraGroup.Core.Application.Responses;

namespace UltraGroup.Presentation.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => (_authService) = authService;


        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<string>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<string>))]
        public async Task<ActionResult> RegisterAsync(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }


        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<AuthenticationResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<string>))]
        public async Task<IActionResult> GetTokenAsync(LoginRequest request)
        {
            var result = await _authService.GetTokenAsync(request);
            return Ok(result);
        }
    }
}
