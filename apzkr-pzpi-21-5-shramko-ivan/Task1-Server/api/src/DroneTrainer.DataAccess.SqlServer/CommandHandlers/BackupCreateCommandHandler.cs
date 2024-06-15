using DroneTrainer.Core.Commands.Maintenace;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class BackupCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<BackupCreateCommand, string>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<string> Handle(BackupCreateCommand request, CancellationToken cancellationToken)
    {
        string fileName = null;
        _dbConnection.Open();

        using (_dbConnection)
        {
            fileName = await CreateBackupFile(request);
            await UpdateBackupHistory(fileName);
        }

        return fileName;
    }

    private async Task<string> CreateBackupFile(BackupCreateCommand request)
    {
        string timeStamp = null;

        var sql = @"
            USE dronetrainer
            declare @credentials_count int = (select count(*) from sys.credentials where name = @StorageUrl)
            
            if @credentials_count = 0 
                declare @command varchar(max) = 'create credential [' 
					+ @StorageUrl + '] with identity = ''' 
					+ @Identity + ''' , secret = ''' 
					+ @Secret + ''';'

                exec(@command)

            ALTER DATABASE dronetrainer
               SET RECOVERY FULL;

            declare @timestamp varchar(50) = convert(varchar, getdate(), 126);
            declare @file_url varchar(100) = concat(@StorageUrl, '/', @timestamp, '.bak');

            BACKUP DATABASE dronetrainer
               TO URL = @file_url;

            set @BackupFileName = concat(@timestamp, '.bak');
        ";

        using (var command = new SqlCommand(sql, _dbConnection))
        {
            var backupFileName = new SqlParameter
            {
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.VarChar,
                ParameterName = "@BackupFileName",
                Size = 50
            };

            command.Parameters.AddRange([
                new("@StorageUrl", request.StorageUrl),
                new("@Identity", request.CredentialIdentity),
                new("@Secret", request.CredentialSecret),
                backupFileName]);

            await command.ExecuteNonQueryAsync();

            timeStamp = (string)backupFileName.Value;
        }

        return timeStamp;
    }

    private async Task UpdateBackupHistory(string fileName)
    {
        var sql = @"insert into backups values(@FileName)";

        using (var command = new SqlCommand(sql, _dbConnection))
        {
            command.Parameters.Add(new("@FileName", fileName));

            await command.ExecuteNonQueryAsync();
        }
    }
}
