using DroneTrainer.Core.Commands;
using DroneTrainer.Core.Queries;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using DroneTrainer.Infrastructure.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class TrainingGroupService(IMediator mediator) : ITraingGroupService
{
    private readonly IMediator _mediator = mediator;

    public async Task<OneOf<
        None,
        UserNotFoundError,
        TrainingGroupNotFoundError,
        TrainingGroupParticipationAlreadyExistsError>>
    AddGroupParticipationAsync(int groupId, TrainingGroupParticipationCreateDTO participationCreateDTO)
    {
        var group = await _mediator.Send(new TrainingGroupQuery(groupId));

        if (group is null) return new TrainingGroupNotFoundError();

        var user = await _mediator.Send(new UserByIdQuery(participationCreateDTO.UserId));

        if (user is null) return new UserNotFoundError();

        var participationExists = await _mediator.Send(new TrainingGroupParticipationCheckQuery(participationCreateDTO.UserId, groupId));

        if (participationExists) return new TrainingGroupParticipationAlreadyExistsError();

        await _mediator.Send(new TrainingGroupParticipationCreateCommand(groupId, participationCreateDTO.UserId));

        return new None();
    }

    public async Task<OneOf<
        None,
        OrganizationNotFoundError,
        TrainingGroupNameNotUniqueError>>
    CreateTrainingGroupAsync(TrainingGroupCreateDTO createDTO)
    {
        var organization = await _mediator.Send(new OrganizationQuery(createDTO.OrganizationId));

        if (organization is null) return new OrganizationNotFoundError();

        var nameExists = await _mediator.Send(new TrainingGroupNameCheckQuery(createDTO.Name, createDTO.OrganizationId));

        if (nameExists) return new TrainingGroupNameNotUniqueError();

        await _mediator.Send(new TrainingGroupCreateCommand(createDTO.Name, createDTO.OrganizationId));

        return new None();
    }
}
