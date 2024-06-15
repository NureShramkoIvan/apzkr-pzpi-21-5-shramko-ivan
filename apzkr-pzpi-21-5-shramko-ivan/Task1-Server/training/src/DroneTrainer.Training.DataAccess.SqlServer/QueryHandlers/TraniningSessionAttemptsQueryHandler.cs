using DroneTrainer.Training.Core.Models;
using DroneTrainer.Training.Core.Queries;
using DroneTrainer.Training.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.Training.DataAccess.SqlServer.QueryHandlers;

internal sealed class TraniningSessionAttemptsQueryHandler(TrainingDbConnection dbConnection)
    : IRequestHandler<TrainingSessionAttemptsQuery, IEnumerable<TrainingSessionAttempt>>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<IEnumerable<TrainingSessionAttempt>> Handle(TrainingSessionAttemptsQuery request, CancellationToken cancellationToken)
    {
        List<TrainingSessionAttempt> attempts = [];

        var sql = @"
            select 
                user_id, 
                started_at, 
                finished_at 
            from user_attempts 
            where session_id = @SessionId
        ";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@SessionId", request.SessionId));

            _dbConnection.Open();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var attempt = new TrainingSessionAttempt
                    {
                        UserId = reader.GetInt32(0),
                        StartedAt = reader.IsDBNull(1)
                            ? null
                            : reader.GetDateTime(1),
                        FinishedAt = reader.IsDBNull(2)
                            ? null
                            : reader.GetDateTime(2)
                    };

                    attempts.Add(attempt);
                }
            }
        }

        return attempts;
    }
}
