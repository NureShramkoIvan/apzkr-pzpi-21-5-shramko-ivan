using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class TrainingGroupNameCheckQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingGroupNameCheckQuery, bool>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<bool> Handle(TrainingGroupNameCheckQuery request, CancellationToken cancellationToken)
    {
        var sql = @"
            select 
                case when exists 
                (
                    select 1 
                    from training_groups
                    where name = @Name
                        AND organization_id = @OrganizationId
                ) 
                then 1
                else 0
            end
        ";

        bool exists;

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            _dbConnection.Open();
            command.Parameters.Add(new("@Name", request.Name));
            command.Parameters.Add(new("@OrganizationId", request.OrganizationId));

            var result = await command.ExecuteScalarAsync(cancellationToken);

            exists = result is not null && Convert.ToBoolean(result);
        }

        return exists;
    }
}
