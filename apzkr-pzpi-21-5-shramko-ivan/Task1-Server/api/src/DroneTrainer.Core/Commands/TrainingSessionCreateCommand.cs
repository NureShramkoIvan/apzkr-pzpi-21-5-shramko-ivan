using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class TrainingSessionCreateCommand(
    DateTime scheduledAt,
    int groupId,
    int programId,
    int instructorId) : IRequest<int>
{
    public DateTime ScheduledAt { get; } = scheduledAt;
    public int GroupId { get; } = groupId;
    public int ProgramId { get; } = programId;
    public int InstructorId { get; } = instructorId;
}
