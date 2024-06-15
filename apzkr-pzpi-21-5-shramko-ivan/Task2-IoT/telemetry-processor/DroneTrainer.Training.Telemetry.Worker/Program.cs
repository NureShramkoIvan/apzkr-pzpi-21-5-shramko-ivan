using DroneTrainer.Training.Telemetry.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.ConfigureWorker();

var host = builder.Build();
host.Run();
