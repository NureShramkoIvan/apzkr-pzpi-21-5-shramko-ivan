using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers.Authentication;

internal sealed class UserCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<UserCreateCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var sql = @"
            insert into users
            (
                username,
                normalized_username,
                password,
                role,
                organization_id,
                time_zone
            )
            values
            (
                @Username,
                @NormalizedUsername,
                @PasswordHash,
                @Role,
                @OrganizationId,
                @TimeZone
            )
        ";

        var command = new SqlCommand(sql, _dbConnection);

        var parameters = new SqlParameter[]
        {
            new("@UserName", request.UserName),
            new("@NormalizedUserName", request.NormalizedUserName),
            new("@PasswordHash", request.PasswordHash),
            new("@Role", request.Role),
            new("@OrganizationId", request.OrganizationId),
            new("@TimeZone", request.UserTimeZone)
    };

        command.Parameters.AddRange(parameters);

        await _dbConnection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        await _dbConnection.CloseAsync();
    }
}
