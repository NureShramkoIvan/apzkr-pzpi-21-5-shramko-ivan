using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class TrainingGroupQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingGroupQuery, TrainingGroup>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<TrainingGroup> Handle(TrainingGroupQuery request, CancellationToken cancellationToken)
    {
        TrainingGroup group = null;

        var sql = "select id, name, organization_id from training_groups where id = @Id";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@Id", request.Id));

            _dbConnection.Open();

            var reader = await command.ExecuteReaderAsync();

            if (reader is not null && reader.HasRows)
            {
                reader.Read();

                group = new()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    OrganizationId = reader.GetInt32(2)
                };

                reader.Close();
            }
        }

        return group;
    }
}
