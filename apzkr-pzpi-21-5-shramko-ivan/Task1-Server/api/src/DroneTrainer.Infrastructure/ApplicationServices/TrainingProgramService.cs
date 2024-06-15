using AutoMapper;
using DroneTrainer.Core.Commands;
using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using DroneTrainer.Infrastructure.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class TrainingProgramService(IMediator mediator, IMapper mapper) : ITrainingProgramService
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    public async Task<OneOf<
        None,
        OrganizationNotFoundError,
        OrganizationDeviceNotFoundError>>
    CreateTrainingProgram(TrainingProgramCreateDTO programCreateDTO)
    {
        var organization = await _mediator.Send(new OrganizationQuery(programCreateDTO.OrganizationId));

        if (organization is null) return new OrganizationNotFoundError();

        var organizationDevices = await _mediator.Send(new OrganizationDevicesQuery(programCreateDTO.OrganizationId));

        var devicesIds = organizationDevices.Select(d => d.Id);

        var devicesExist = programCreateDTO.Steps.Select(s => s.DeviceId).All(devicesIds.Contains);

        if (!devicesExist) return new OrganizationDeviceNotFoundError();

        await _mediator.Send(new TrainingProgramCreateCommand(
            programCreateDTO.OrganizationId,
            programCreateDTO.Steps.Select(_mapper.Map<TrainingProgramStep>)));

        return new None();
    }
}
