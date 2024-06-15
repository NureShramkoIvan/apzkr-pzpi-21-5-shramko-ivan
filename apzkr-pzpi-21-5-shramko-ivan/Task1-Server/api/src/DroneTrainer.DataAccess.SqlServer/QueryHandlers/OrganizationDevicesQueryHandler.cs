using DroneTrainer.Core.Enums;
using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class OrganizationDevicesQueryHandler(DroneTrainerDbConnection dbConnection)
    : IRequestHandler<OrganizationDevicesQuery, IEnumerable<OrganizationDevice>>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<IEnumerable<OrganizationDevice>> Handle(OrganizationDevicesQuery request, CancellationToken cancellationToken)
    {
        var devices = new List<OrganizationDevice>();

        var sql = "select id, type, organization_id from devices where organization_id = @OrganizationId";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            _dbConnection.Open();
            command.Parameters.Add(new("@OrganizationId", request.OrganizationId));

            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                devices.Add(new OrganizationDevice
                {
                    Id = reader.GetInt32(0),
                    Type = (DeviceType)reader.GetInt32(1),
                    OrganizationId = reader.GetInt32(2)
                });
            }

            reader.Close();
        }

        return devices;
    }
}
