using AutoMapper;
using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using DroneTrainer.Api.ViewModels;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Api.Controllers;

[Route("api/training-sessions")]
[Authorize]
public sealed class TrainingSessionController(
    IMapper mapper,
    ITrainingSessionService trainingSessionService,
    ErrorMessageLocalizer errorMessageLocalizer) : AppController
{
    private readonly IMapper _mapper = mapper;
    private readonly ITrainingSessionService _trainingSessionService = trainingSessionService;
    private readonly ErrorMessageLocalizer _errorMessageLocalizer = errorMessageLocalizer;

    [HttpPost]
    public async Task<IActionResult> CreateTrainingSession([FromBody] TrainingSessionCreateVM sessionCreateVM)
    {
        var createResult = await _trainingSessionService.CreateTrainingSessionAsync(_mapper.Map<TrainingSessionCreateDTO>(sessionCreateVM));

        return createResult.Match<IActionResult>(
            sessionId => Ok(sessionId),
            programNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.TrainingProgramNotFound)),
            instructorNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.InstructorNotFound)),
            groupNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.TrainingGroupNotFound)));
    }
}
