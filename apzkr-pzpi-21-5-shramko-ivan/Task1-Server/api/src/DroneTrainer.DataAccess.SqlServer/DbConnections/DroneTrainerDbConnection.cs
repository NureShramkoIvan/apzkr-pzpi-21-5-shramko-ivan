using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.DbConnections;

internal sealed class DroneTrainerDbConnection(string connectionString)
{
    public SqlConnection Connection { get; } = new SqlConnection(connectionString);
}
