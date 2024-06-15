using DroneTrainer.Training.Telemetry.Core.Commands;
using DroneTrainer.Training.Telemetry.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.Training.Telemetry.DataAccess.SqlServer.CommandHandlers;

internal sealed class StepCompletionCommandHandler(TrainingDbConnection dbConnection) : IRequestHandler<StepCompletionCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(StepCompletionCommand request, CancellationToken cancellationToken)
    {
        var sql = "update attempt_steps set passed_at = @PassedAt where device_unique_id = @DeviceId and attempt_id = @AttemptId";

        _dbConnection.Open();

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.AddRange([
                new("@PassedAt", request.PassedAt),
                new("@DeviceId", request.DeviceId),
                new("@AttemptId", request.AttemptId)]);

            await command.ExecuteNonQueryAsync();
        }
    }
}
