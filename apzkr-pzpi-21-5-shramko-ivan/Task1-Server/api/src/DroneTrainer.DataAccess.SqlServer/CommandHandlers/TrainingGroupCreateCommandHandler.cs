using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class TrainingGroupCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingGroupCreateCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(TrainingGroupCreateCommand request, CancellationToken cancellationToken)
    {
        var sql = @"
            insert into training_groups
            (
                name,
                organization_id
            )
            values
            (
                @Name,
                @OrganizationId
            )
        ";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@Name", request.Name));
            command.Parameters.Add(new("@OrganizationId", request.OrganizationId));

            _dbConnection.Open();
            await command.ExecuteNonQueryAsync();
        };
    }
}
