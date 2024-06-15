using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class OrganizationDeviceCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<OrganizationDeviceCreateCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(OrganizationDeviceCreateCommand request, CancellationToken cancellationToken)
    {
        var sql = @"
            insert into devices
            (
                type,
                organization_id
            )
            values
            (
                @Type,
                @OrganizationId
            )
        ";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.AddRange([new("@Type", request.Type), new("@OrganizationId", request.OrganizationId)]);

            _dbConnection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }
}
