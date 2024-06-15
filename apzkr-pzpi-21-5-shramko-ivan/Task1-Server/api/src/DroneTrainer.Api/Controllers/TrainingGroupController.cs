using AutoMapper;
using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using DroneTrainer.Api.ViewModels;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Api.Controllers;

[Route("api/training-groups")]
[Authorize]
public sealed class TrainingGroupController(
    IMapper mapper,
    ITraingGroupService traingGroupService,
    ErrorMessageLocalizer errorMessageLocalizer) : AppController
{
    private readonly IMapper _mapper = mapper;
    private readonly ITraingGroupService _traingGroupService = traingGroupService;
    private readonly ErrorMessageLocalizer _errorMessageLocalizer = errorMessageLocalizer;

    [HttpPost]
    public async Task<IActionResult> CreateTraingGroup([FromBody] TrainingGroupCreateVM createVM)
    {
        var createResult = await _traingGroupService.CreateTrainingGroupAsync(_mapper.Map<TrainingGroupCreateDTO>(createVM));

        return createResult.Match<IActionResult>(
            created => Ok(),
            organizationNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.OrganizationNotFound)),
            nameNotUnique => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.TrainingGroupNameNotUnique)));
    }

    [HttpPost("{group_id}/participations")]
    public async Task<IActionResult> AddGroupParticipation(
        [FromRoute(Name = "group_id")] int groupId,
        [FromBody] TrainingGroupParticipationCreateVM participationCreateVM)
    {
        var addResult = await _traingGroupService.AddGroupParticipationAsync(
            groupId,
            _mapper.Map<TrainingGroupParticipationCreateDTO>(participationCreateVM));

        return addResult.Match<IActionResult>(
            participationCreated => Ok(),
            userNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.UserNotFound)),
            groupNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.TrainingGroupNotFound)),
            participationAlreadyExists => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.TrainingGroupParticipationAlreadyExists)));
    }
}
