using DroneTrainer.Api.Constants;
using DroneTrainer.Api.DependencyModules;
using DroneTrainer.Api.Localization.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(
    builder.Configuration.GetRequiredSection(ConfigurationPaths.JWT),
    builder.Configuration[ConfigurationPaths.TrainerDB],
    builder.Configuration.GetSection(ConfigurationPaths.BackupCreate),
    builder.Configuration.GetSection(ConfigurationPaths.BackupRead));

builder.Services.AddResponseLocalization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseLocalization();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
