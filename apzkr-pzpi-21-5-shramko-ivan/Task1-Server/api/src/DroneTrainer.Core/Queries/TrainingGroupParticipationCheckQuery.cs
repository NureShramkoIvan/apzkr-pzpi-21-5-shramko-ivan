using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class TrainingGroupParticipationCheckQuery(int userId, int groupId) : IRequest<bool>
{
    public int UserId { get; } = userId;
    public int GroupId { get; } = groupId;
}
