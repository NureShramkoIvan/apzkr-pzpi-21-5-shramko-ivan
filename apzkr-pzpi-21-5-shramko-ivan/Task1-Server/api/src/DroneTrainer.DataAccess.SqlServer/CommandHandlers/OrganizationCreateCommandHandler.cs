using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class OrganizationCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<OrganizationCreateCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(OrganizationCreateCommand request, CancellationToken cancellationToken)
    {
        var sql = @"
            insert into organizations
            (
                name
            )
            values
            (
                @Name
            )
        ";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@Name", request.Name));

            _dbConnection.Open();
            await command.ExecuteNonQueryAsync();
        };
    }
}
