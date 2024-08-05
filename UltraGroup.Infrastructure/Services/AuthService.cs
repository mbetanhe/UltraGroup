using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Domain.Settings;

namespace UltraGroup.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwt;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<IResult<string>> RegisterAsync(RegisterRequest data)
        {
            IdentityUser user = new IdentityUser
            {
                Email = data.Email,
                UserName = data.Email,
                PhoneNumber = data.PhoneNumber
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(data.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, data.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Basic");

                }
                return Result<string>.Success(user.Id, $"User has been register successfully {user.UserName}");
            }
            else
            {
                return Result<string>.Fail($"User already exists");
            }
        }
        public async Task<AuthenticationResponse> GetTokenAsync(LoginRequest data)
        {
            AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null)
            {
                authenticationResponse.IsAuthenticated = false;
                authenticationResponse.Message = $"User {data.Email} does not exists.";
                // return Result<AuthenticationResponse>.Success(authenticationResponse);
                return authenticationResponse;

            }
            if (await _userManager.CheckPasswordAsync(user, data.Password))
            {
                authenticationResponse.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationResponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationResponse.Email = user.Email;
                authenticationResponse.Id = user.Id;
                authenticationResponse.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationResponse.Roles = rolesList.ToList();
                //return Result<AuthenticationResponse>.Success(authenticationResponse);
                return authenticationResponse;
            }
            authenticationResponse.IsAuthenticated = false;
            authenticationResponse.Message = $"Incorrect credentials for {user.Email}.";
            //return Result<AuthenticationResponse>.Success(authenticationResponse);

            return authenticationResponse;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(IdentityUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
    }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
}
