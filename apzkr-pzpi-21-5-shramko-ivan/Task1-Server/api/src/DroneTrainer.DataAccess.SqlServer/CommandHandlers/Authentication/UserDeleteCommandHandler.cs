using DroneTrainer.Core.Commands;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers.Authentication;

internal sealed class UserDeleteCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<UserDeleteCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        var sql = "delete from users where id = @Id";

        var command = new SqlCommand(sql, _dbConnection);

        var paramters = new SqlParameter[] { new("@Id", request.Id) };

        command.Parameters.Add(paramters);

        _dbConnection.Open();
        await command.ExecuteNonQueryAsync();
        _dbConnection.Close();
    }
}
