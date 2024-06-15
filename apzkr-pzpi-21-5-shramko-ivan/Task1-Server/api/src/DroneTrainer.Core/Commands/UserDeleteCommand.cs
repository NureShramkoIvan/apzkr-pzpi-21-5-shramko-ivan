using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class UserDeleteCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
