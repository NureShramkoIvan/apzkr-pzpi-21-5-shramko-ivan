using DroneTrainer.Mobile.Core.Models;

namespace DroneTrainer.Mobile.Services.Interfaces;

public interface ITrainingProgramService
{
    Task<TrainingProgram> GetTrainingProgram(int id);
}
