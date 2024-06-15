using DroneTrainer.Core.Commands;
using MediatR;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers.Authentication;

internal sealed class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand>
{
    public Task Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
