using AutoMapper;
using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using DroneTrainer.Api.ViewModels;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Api.Controllers;

[Route("api/organizations")]
[Authorize]
public sealed class OrganizationController(
    IOrganizationService organizationService,
    IMapper mapper,
    ErrorMessageLocalizer errorMessageLocalizer) : AppController
{
    private readonly IOrganizationService _organizationService = organizationService;
    private readonly IMapper _mapper = mapper;
    private readonly ErrorMessageLocalizer _errorMessageLocalizer = errorMessageLocalizer;

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationCreateVM organizationCreateVM)
    {
        var createResult = await _organizationService.CreateOrganization(_mapper.Map<OrganizationCreateDTO>(organizationCreateVM));

        return createResult.Match<IActionResult>(
            organizationCreated => Ok(),
            nameNotUnique => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.OrganizationNameNotUnique)));
    }

    [HttpPost("{organization_id}/devices")]
    public async Task<IActionResult> AddOrganizationDevice(
        [FromRoute(Name = "organization_id")] int organizationId,
        [FromBody] OrganizationDeviceCreateVM deviceCreateVM)
    {
        var createResult = await _organizationService.AddOrganizationDeviceAsync(_mapper.Map<OrganizationDeviceCreateDTO>(deviceCreateVM), organizationId);

        return createResult.Match<IActionResult>(
            deviceAdded => Ok(),
            organizationNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.OrganizationNotFound)));
    }

    [HttpGet("{organization_id}/devices")]
    public async Task<IActionResult> GetOrganizationDeviceList([FromRoute(Name = "organization_id")] int organizationId)
    {
        var getResult = await _organizationService.GetOrganizationDevicesList(organizationId);

        return getResult.Match<IActionResult>(
            devicesList => Ok(devicesList.Select(_mapper.Map<OrganizationDeviceVM>)),
            organizationNotFound => BadRequest(_errorMessageLocalizer.GetLocalizedErrorMessage(LocalizationResourseKeys.OrganizationNotFound)));
    }
}
