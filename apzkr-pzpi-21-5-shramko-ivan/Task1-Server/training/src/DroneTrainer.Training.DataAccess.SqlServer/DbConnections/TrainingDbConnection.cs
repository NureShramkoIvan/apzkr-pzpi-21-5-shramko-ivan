using Microsoft.Data.SqlClient;

namespace DroneTrainer.Training.DataAccess.SqlServer.DbConnections;

internal sealed class TrainingDbConnection(string connectionString)
{
    public SqlConnection Connection { get; } = new SqlConnection(connectionString);
}
