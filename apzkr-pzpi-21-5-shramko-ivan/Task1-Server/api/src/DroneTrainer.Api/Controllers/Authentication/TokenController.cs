using AutoMapper;
using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using DroneTrainer.Api.ViewModels.Authentication;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Api.Controllers.Authentication;

[Route("api")]
public sealed class TokenController(
    IAuthService authService,
    IMapper mapper,
    ErrorMessageLocalizer errorMessageLocalizer) : AppController
{
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;
    private readonly ErrorMessageLocalizer _errorMessageLocalizer = errorMessageLocalizer;

    /// <summary>
    /// Get access token.
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    [HttpPost("token")]
    [AllowAnonymous]
    public async Task<IActionResult> Token(TokenGetVM vm)
    {
        var tokenGetResult = await _authService.GetAccessTokenAsync(vm.UserName, vm.Password);

        return tokenGetResult.Match<IActionResult>(
            accesToken => Ok(_mapper.Map<AccessTokenVM>(accesToken)),
            userNotFound => NotFound(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.UserNotFound)),
            invalidCredentials => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.InvalidCredentials)));
    }
}
