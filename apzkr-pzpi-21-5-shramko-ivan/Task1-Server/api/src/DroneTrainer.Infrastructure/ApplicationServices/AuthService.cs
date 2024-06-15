using DroneTrainer.Core.Models;
using DroneTrainer.Infrastructure.Constants;
using DroneTrainer.Infrastructure.DTOs.Authenitication;
using DroneTrainer.Infrastructure.Errors;
using DroneTrainer.Infrastructure.Interfaces;
using DroneTrainer.Infrastructure.Settings;
using DroneTrainer.Shared.Dates.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class AuthService(
    UserManager<User> userManager,
    IOptions<IdentitySettings> identitySettings,
    IDateConverterService dateConverterService) : IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IDateConverterService _dateConverterService = dateConverterService;
    private readonly IdentitySettings _identitySettings = identitySettings.Value;

    public async Task<OneOf<
        AccessTokenDTO,
        UserNotFoundError,
        InvalidCredentialsError>>
    GetAccessTokenAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null) return new UserNotFoundError();

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!isPasswordValid) return new InvalidCredentialsError();

        var claims = GetUserClaims(user);

        return GenerateAccessToken(claims);
    }

    private IEnumerable<Claim> GetUserClaims(User user)
    {
        return new List<Claim>
        {
            new(Claims.RoleId, user.Role.ToString()),
            new(Claims.OrganizationId, user.OrganizationId.ToString()),
            new(Claims.TimeZoneOffset, _dateConverterService.GetSupportedTimeZoneOffset(user.UserTimeZone).ToString())
        };
    }

    private AccessTokenDTO GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var jwtToken = new JwtSecurityToken(
            issuer: _identitySettings.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_identitySettings.TokenExpiresAfterSeconds),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identitySettings.SecretKey)),
                SecurityAlgorithms.HmacSha256));

        return new()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            ExpiresIn = _identitySettings.TokenExpiresAfterSeconds
        };
    }
}
