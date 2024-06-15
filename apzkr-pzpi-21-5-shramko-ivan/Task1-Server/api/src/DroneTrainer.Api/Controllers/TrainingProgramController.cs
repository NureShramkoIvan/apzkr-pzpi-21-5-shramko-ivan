using AutoMapper;
using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using DroneTrainer.Api.ViewModels;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Api.Controllers;

[Route("api/training-programs")]
[Authorize]
public sealed class TrainingProgramController(
    IMapper mapper,
    ITrainingProgramService trainingProgramService,
    ErrorMessageLocalizer errorMessageLocalizer) : AppController
{
    private readonly IMapper _mapper = mapper;
    private readonly ITrainingProgramService _trainingProgramService = trainingProgramService;
    private readonly ErrorMessageLocalizer _errorMessageLocalizer = errorMessageLocalizer;

    [HttpPost]
    public async Task<IActionResult> CreateTrainingProgram([FromBody] TrainingProgramCreateVM programCreateVM)
    {
        var createResult = await _trainingProgramService.CreateTrainingProgram(_mapper.Map<TrainingProgramCreateDTO>(programCreateVM));
        return createResult.Match<IActionResult>(
            programCreated => Ok(),
            ogranizationNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.OrganizationNotFound)),
            deviceNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.OrganizationDeviceNotFound)));
    }
}
