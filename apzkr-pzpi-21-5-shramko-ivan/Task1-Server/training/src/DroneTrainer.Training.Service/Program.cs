using DroneTrainer.Training.Infrastructure;
using DroneTrainer.Training.Service.Constants;
using DroneTrainer.Training.Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

builder.Services.AddInfrastructure(builder.Configuration[ConfigurationPaths.TrainingDb]);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGrpcService<TrainingResultService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
