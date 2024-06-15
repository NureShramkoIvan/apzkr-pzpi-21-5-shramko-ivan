using DroneTrainer.Training.Telemetry.Core.Commands;
using DroneTrainer.Training.Telemetry.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.Training.Telemetry.DataAccess.SqlServer.CommandHandlers;

internal sealed class SessionStepAvarageCompletionTimeCreateCommadHandler(TrainingDbConnection dbConnection) : IRequestHandler<SessionStepAvarageCompletionTimeCreateCommad>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(SessionStepAvarageCompletionTimeCreateCommad request, CancellationToken cancellationToken)
    {
        var sql = @"
            insert into step_avarage_completion_time
            (
                session_id,
                device_unique_id,
                avarage_time
            )
            values
            (
                @SessionId,
                @DeviceId,
                @AvarageTime
            )
        ";

        _dbConnection.Open();

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.AddRange([
                new("@SessionId", request.SesisonId),
                new("@DeviceId", request.DeviceId),
                new("@AvarageTime", request.AvgTime)]);

            await command.ExecuteNonQueryAsync();
        }
    }
}
