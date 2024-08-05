using UltraGroup.Core.Application.Requests;
using UltraGroup.Core.Application.Responses;

namespace UltraGroup.Core.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> GetTokenAsync(LoginRequest data);
        Task<IResult<string>> RegisterAsync(RegisterRequest data);
    }
}