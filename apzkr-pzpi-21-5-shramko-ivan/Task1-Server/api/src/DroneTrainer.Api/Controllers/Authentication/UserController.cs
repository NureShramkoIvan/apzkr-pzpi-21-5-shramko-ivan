using AutoMapper;
using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using DroneTrainer.Api.ViewModels;
using DroneTrainer.Api.ViewModels.Authentication;
using DroneTrainer.Infrastructure.DTOs.Authenitication;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Api.Controllers.Authentication;

[Route("api/users")]
[Authorize]
public sealed class UserController(
    IUserService userService,
    IMapper mapper,
    ITrainingSessionService trainingSessionService,
    ErrorMessageLocalizer errorMessageLocalizer) : AppController
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;
    private readonly ITrainingSessionService _trainingSessionService = trainingSessionService;
    private readonly ErrorMessageLocalizer _errorMessageLocalizer = errorMessageLocalizer;

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerVM"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterVM vm)
    {
        var createResult = await _userService.RegisterUser(_mapper.Map<UserRegisterDTO>(vm));

        return createResult.Match<IActionResult>(
            userCreated => Ok(),
            failedTocreateUser => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.FailedToRegisterUser)));
    }

    [HttpGet("{user_id}/training-sessions")]
    public async Task<IActionResult> GetUserTrainingSessions([FromRoute(Name = "user_id")] int userId)
    {
        var sessionsGetResult = await _trainingSessionService.GetUserTrainingSessions(userId);

        return sessionsGetResult.Match<IActionResult>(
            userSessions => Ok(userSessions.Select(_mapper.Map<TrainingSessionVM>)),
            userNotFound => NotFound(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.UserNotFound)));
    }

    [HttpGet("{user_id}/training-sessions/{session_id}/result")]
    public async Task<IActionResult> GetUserTRainingSesisonResult(
        [FromRoute(Name = "user_id")] int userId,
        [FromRoute(Name = "session_id")] int sessionId)
    {
        var userSessionResult = await _trainingSessionService.GetUserTrainingSessionResult(userId, sessionId);
        return Ok(_mapper.Map<UserTrainingSessionResultVM>(userSessionResult));
    }
}