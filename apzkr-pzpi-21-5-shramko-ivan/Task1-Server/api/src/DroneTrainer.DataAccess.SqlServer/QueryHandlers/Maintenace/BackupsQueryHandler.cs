using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries.Maintenace;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers.Maintenace;

internal sealed class BackupsQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<BackupsQuery, IEnumerable<Backup>>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<IEnumerable<Backup>> Handle(BackupsQuery request, CancellationToken cancellationToken)
    {
        var backups = new List<Backup>();

        var sql = "select id, file_name from backups";

        using (_dbConnection)
        using (var command = new SqlCommand(sql, _dbConnection))
        {
            _dbConnection.Open();

            var reader = await command.ExecuteReaderAsync();

            using (reader)
            {
                while (reader.Read())
                {
                    var backup = new Backup
                    {
                        Id = reader.GetInt32(0),
                        FileName = reader.GetString(1)
                    };

                    backups.Add(backup);
                }
            }
        }

        return backups;
    }
}
