using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface ITraingGroupService
{
    Task<OneOf<
        None,
        OrganizationNotFoundError,
        TrainingGroupNameNotUniqueError>>
    CreateTrainingGroupAsync(TrainingGroupCreateDTO createDTO);

    Task<OneOf<
        None,
        UserNotFoundError,
        TrainingGroupNotFoundError,
        TrainingGroupParticipationAlreadyExistsError>>
    AddGroupParticipationAsync(int groupId, TrainingGroupParticipationCreateDTO participationCreateDTO);
}
