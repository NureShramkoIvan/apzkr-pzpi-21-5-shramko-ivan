using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class OrganizationQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<OrganizationQuery, Organization>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<Organization> Handle(OrganizationQuery request, CancellationToken cancellationToken)
    {
        Organization organization = null;

        var sql = "select id, name from organizations where id = @Id";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@Id", request.Id));

            _dbConnection.Open();
            var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                await reader.ReadAsync();

                organization = new()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            };
        }

        return organization;
    }
}
