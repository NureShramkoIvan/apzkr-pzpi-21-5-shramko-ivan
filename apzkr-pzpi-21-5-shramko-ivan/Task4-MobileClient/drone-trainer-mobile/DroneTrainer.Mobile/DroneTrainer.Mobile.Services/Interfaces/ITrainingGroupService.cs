using DroneTrainer.Mobile.Core.Models;

namespace DroneTrainer.Mobile.Services.Interfaces;

public interface ITrainingGroupService
{
    Task<IEnumerable<TrainingGroupParticipation>> GetGroupParticipations(int groupId);
}
