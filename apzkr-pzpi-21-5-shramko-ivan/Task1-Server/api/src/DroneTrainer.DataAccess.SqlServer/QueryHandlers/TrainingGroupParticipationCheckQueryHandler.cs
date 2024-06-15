using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class TrainingGroupParticipationCheckQueryHandler(DroneTrainerDbConnection dbConnection)
    : IRequestHandler<TrainingGroupParticipationCheckQuery, bool>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<bool> Handle(TrainingGroupParticipationCheckQuery request, CancellationToken cancellationToken)
    {
        bool exists;

        var sql = @"
            select 
                case when exists 
                (
                    select 1 
                    from group_users
                    where group_id = @GroupId
                          and user_id = @UserId
                ) 
                then 1 
                else 0
            end
        ";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.AddRange([new("@GroupId", request.GroupId), new("@UserId", request.UserId)]);

            _dbConnection.Open();

            var result = await command.ExecuteScalarAsync();

            exists = result is not null && Convert.ToBoolean(result);
        }

        return exists;
    }
}
