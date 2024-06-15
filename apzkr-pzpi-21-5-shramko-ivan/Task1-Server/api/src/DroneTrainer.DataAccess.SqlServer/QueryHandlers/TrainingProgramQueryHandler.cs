using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using DroneTrainer.DataAccess.SqlServer.DbConnections;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DroneTrainer.DataAccess.SqlServer.QueryHandlers;

internal sealed class TrainingProgramQueryHandler(DroneTrainerDbConnection dbConnection) : IRequestHandler<TrainingProgramQuery, TrainingProgram>
{
    private readonly SqlConnection _dbConnection = dbConnection.Connection;

    public async Task<TrainingProgram> Handle(TrainingProgramQuery request, CancellationToken cancellationToken)
    {
        List<Record> records = [];

        var sql = @"
            select 
            	tp.id,
            	tp.organization_id,
            	tps.id,
            	tps.device_id,
            	tps.position 
            from training_programs as tp 
            join training_program_steps as tps 
            	on tps.program_id = tp.id and tp.id = @Id
        ";

        using (_dbConnection)
        using (var commad = new SqlCommand(sql, _dbConnection))
        {
            commad.Parameters.Add(new("@Id", request.Id));

            _dbConnection.Open();

            var reader = await commad.ExecuteReaderAsync();

            if (reader is not null && reader.HasRows)
            {
                while (reader.Read())
                {
                    var record = new Record
                    {
                        TrainingProgramId = reader.GetInt32(0),
                        OrganizationId = reader.GetInt32(1),
                        StepId = reader.GetInt32(2),
                        DeviceId = reader.GetInt32(3),
                        Position = reader.GetInt32(4)
                    };

                    records.Add(record);
                }

                reader.Close();
            }
        }

        return records
            .GroupBy(r => new
            {
                TrainingProgramId = r.TrainingProgramId,
                OrganizationId = r.OrganizationId
            })
            .Select((group) => new TrainingProgram
            {
                Id = group.Key.TrainingProgramId,
                OrganizationId = group.Key.OrganizationId,
                Steps = group.Select(gi => new TrainingProgramStep
                {
                    Id = gi.StepId,
                    DeviceId = gi.DeviceId,
                    Position = gi.Position,
                    ProgramId = gi.TrainingProgramId
                })
            })
            .SingleOrDefault();
    }

    private sealed class Record
    {
        public int TrainingProgramId { get; set; }
        public int OrganizationId { get; set; }
        public int StepId { get; set; }
        public int DeviceId { get; set; }
        public int Position { get; set; }
    }
}
