using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class TrainingSessionCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingSessionCreateCommand, int>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<int> Handle(TrainingSessionCreateCommand request, CancellationToken cancellationToken)
    {
        var sessionId = 0;

        var sql = @"
            insert into training_sessions 
            (scheduled_at, program_id, group_id, instructor_id)
            values (@ScheduledAt, @ProgramId, @GroupId, @InstructorId);
            set @Id = SCOPE_IDENTITY()
        ";

        using (_dbConnection)
        using (var commad = new SqlCommand(sql, _dbConnection))
        {
            var returnParamter = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            commad.Parameters.AddRange([
                new("@ScheduledAt", request.ScheduledAt),
                new("@ProgramId", request.ProgramId),
                new("@GroupId", request.GroupId),
                new("@InstructorId", request.InstructorId),
                returnParamter
            ]);

            _dbConnection.Open();

            await commad.ExecuteNonQueryAsync();

            sessionId = (int)returnParamter.Value;
        }

        return sessionId;
    }
}
