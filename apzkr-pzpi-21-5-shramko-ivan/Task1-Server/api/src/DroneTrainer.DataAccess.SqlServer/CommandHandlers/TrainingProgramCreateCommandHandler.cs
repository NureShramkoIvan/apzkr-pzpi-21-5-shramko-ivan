using DroneTrainer.Core.Commands;
using DroneTrainer.Core.Models;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DroneTrainer.DataAccess.SqlServer.CommandHandlers;

internal sealed class TrainingProgramCreateCommandHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingProgramCreateCommand>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task Handle(TrainingProgramCreateCommand request, CancellationToken cancellationToken)
    {
        _dbConnection.Open();
        var transaction = _dbConnection.BeginTransaction();

        try
        {
            var programId = await CreateTrainingProgramAsync(request.OrganizationId, transaction);
            await CreateTrainingProgramStepsAsync(
                request.Steps,
                transaction,
                programId);

            transaction.Commit();
            _dbConnection.Close();
        }
        catch (Exception)
        {
            transaction.Rollback();
            _dbConnection.Close();
        }
    }

    private async Task CreateTrainingProgramStepsAsync(
        IEnumerable<TrainingProgramStep> steps,
        SqlTransaction transaction,
        int programId)
    {
        var sql = "insert into training_program_steps values (@DeviceId, @Position, @ProgramId)";

        foreach (var step in steps)
        {
            using (var commad = new SqlCommand(sql, _dbConnection, transaction))
            {
                commad.Parameters.AddRange([
                    new("@DeviceId", step.DeviceId),
                    new("@Position", step.Position),
                    new("@ProgramId", programId)]);

                await commad.ExecuteNonQueryAsync();
            }
        }
    }

    private async Task<int> CreateTrainingProgramAsync(int organizationId, SqlTransaction transaction)
    {
        var programId = 0;

        var sql = "insert into training_programs values (@OrganizationId); set @ProgramId = SCOPE_IDENTITY()";

        using (var command = new SqlCommand(sql, _dbConnection, transaction))
        {
            var programIdParameter = new SqlParameter
            {
                ParameterName = "@ProgramId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Output
            };

            command.Parameters.AddRange([new("@OrganizationId", organizationId), programIdParameter]);

            await command.ExecuteNonQueryAsync();

            programId = (int)programIdParameter.Value;
        }

        return programId;
    }
}
