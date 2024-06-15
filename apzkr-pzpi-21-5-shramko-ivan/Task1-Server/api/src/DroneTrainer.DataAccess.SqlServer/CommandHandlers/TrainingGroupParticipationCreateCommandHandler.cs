using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class TrainingGroupParticipationCreateCommandHandler(DroneTrainerDbConnection dbConnection)
    : IRequestHandler<TrainingGroupParticipationCreateCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(TrainingGroupParticipationCreateCommand request, CancellationToken cancellationToken)
    {
        var sql = "insert into group_users values(@UserId, @GroupId)";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.AddRange([new("@GroupId", request.GroupId), new("@UserId", request.UserId)]);

            _dbConnection.Open();

            await command.ExecuteNonQueryAsync();
        }
    }
}
