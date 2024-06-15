using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface ITrainingProgramService
{
    Task<OneOf<
        None,
        OrganizationNotFoundError,
        OrganizationDeviceNotFoundError>>
    CreateTrainingProgram(TrainingProgramCreateDTO programCreateDTO);
}
