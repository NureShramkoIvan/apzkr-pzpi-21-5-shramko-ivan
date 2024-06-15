using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class TrainingSessionsQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingSessionsQuery, IEnumerable<TrainingSession>>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<IEnumerable<TrainingSession>> Handle(TrainingSessionsQuery request, CancellationToken cancellationToken)
    {
        List<TrainingSession> sessions = [];

        var filterByUserQueryPart = @"
            where group_id in 
            (
                select group_id 
                from group_users 
                where user_id = @UserId
            )
        ";

        var sql = @$"
            select
                id,
                program_id,
                group_id,
                instructor_id,
                scheduled_at,
                started_at,
                finished_at
            from training_sessions
            {(request.UserId is not null
                ? filterByUserQueryPart
                : string.Empty)}
            ";

        using (_dbConnection)
        using (var commad = new SqlCommand(sql, _dbConnection))
        {
            commad.Parameters.Add(new("@UserId", request.UserId));

            _dbConnection.Open();

            using (var reader = await commad.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var session = new TrainingSession
                    {
                        Id = reader.GetInt32(0),
                        ProgramId = reader.GetInt32(1),
                        GroupId = reader.GetInt32(2),
                        InstructorId = reader.GetInt32(3),
                        ScheduledAt = reader.GetDateTime(4),
                        StartedAt = reader.IsDBNull(5)
                            ? null
                            : reader.GetDateTime(5),
                        FinishedAt = reader.IsDBNull(6)
                            ? null
                            : reader.GetDateTime(6)
                    };

                    sessions.Add(session);
                }
            }
        }

        return sessions;
    }
}
