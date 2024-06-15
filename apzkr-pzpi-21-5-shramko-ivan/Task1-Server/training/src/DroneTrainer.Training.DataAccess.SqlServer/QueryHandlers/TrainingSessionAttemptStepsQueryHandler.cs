using DroneTrainer.Training.Core.Models;
using DroneTrainer.Training.Core.Queries;
using DroneTrainer.Training.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.Training.DataAccess.SqlServer.QueryHandlers;

internal sealed class TrainingSessionAttemptStepsQueryHandler(TrainingDbConnection dbConnection)
    : IRequestHandler<TrainingSessionAttemptStepsQuery, IEnumerable<TrainingSessionAttemptStep>>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<IEnumerable<TrainingSessionAttemptStep>> Handle(TrainingSessionAttemptStepsQuery request, CancellationToken cancellationToken)
    {
        List<TrainingSessionAttemptStep> steps = [];

        var sql = @"
            select uat.user_id,
                   ats.device_id,
                   ats.passed_at
            from user_attempts as uat 
            left join attempt_steps as ats 
                on uat.id = ats.attempt_id 
                   and uat.session_id = @SessionId
        ";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@SessionId", request.SessionId));

            _dbConnection.Open();

            using var reader = await command.ExecuteReaderAsync();
            {
                while (reader.Read())
                {
                    var step = new TrainingSessionAttemptStep
                    {
                        ParticipantId = reader.GetInt32(0),
                        GateId = reader.GetInt32(1),
                        PassedAt = reader.IsDBNull(2)
                            ? null
                            : reader.GetDateTime(2)
                    };

                    steps.Add(step);
                }
            }
        }

        return steps;
    }
}
