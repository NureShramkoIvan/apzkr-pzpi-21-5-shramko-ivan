using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class TrainingGroupParticipationCreateCommand(int groupId, int userId) : IRequest
{
    public int GroupId { get; } = groupId;
    public int UserId { get; } = userId;
}
