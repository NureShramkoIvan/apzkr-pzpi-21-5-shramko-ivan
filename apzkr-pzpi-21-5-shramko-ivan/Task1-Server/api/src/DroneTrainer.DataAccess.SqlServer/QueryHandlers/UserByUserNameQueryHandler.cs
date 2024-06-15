using DroneTrainer.Core.Enums;
using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using DroneTrainer.Shared.Dates.Types.Enums;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class UserByUserNameQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<UserByUserNameQuery, User>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<User> Handle(UserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var sql = @"
            select  
                id,
                username,
                normalized_username,
                password,
                organization_id,
                role,
                time_zone
            from users 
            where normalized_username = @NormalizedUserName
        ";

        var command = new SqlCommand(sql, _dbConnection);

        command.Parameters.Add(new("@NormalizedUserName", request.NormalizedUserName));

        _dbConnection.Open();
        var reader = await command.ExecuteReaderAsync(cancellationToken);

        User user = null;

        if (reader.HasRows)
        {
            await reader.ReadAsync(cancellationToken);

            user = new()
            {
                Id = reader.GetInt32(0),
                UserName = reader.GetString(1),
                NormalizedUserName = reader.GetString(2),
                Password = reader.GetString(3),
                OrganizationId = reader.GetInt32(4),
                Role = (Role)reader.GetInt32(5),
                UserTimeZone = (SupportedTimeZone)reader.GetInt32(6)
            };
        }

        reader.Close();
        _dbConnection.Close();

        return user;
    }
}
