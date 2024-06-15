using AutoMapper;
using DroneTrainer.Training.Telemetry.Inftrastructure.DTOs;
using DroneTrainer.Training.Telemetry.Inftrastructure.Interfaces;
using DroneTrainer.Training.Telemetry.Worker.Constants;
using DroneTrainer.Training.Telemetry.Worker.Interfaces;
using DroneTrainer.Training.Telemetry.Worker.Models;
using System.Text.Json;

namespace DroneTrainer.Training.Telemetry.Worker.Services;

internal sealed class TelemetryProcessor(
    IQueueService queueService,
    ILogger<TelemetryProcessor> logger,
    IDeviceTelemetryService deviceTelemetryService,
    IMapper mapper) : ITelemetryProcessor
{
    private readonly IQueueService _queueService = queueService;
    private readonly ILogger<TelemetryProcessor> _logger = logger;
    private readonly IDeviceTelemetryService _deviceTelemetryService = deviceTelemetryService;
    private readonly IMapper _mapper = mapper;

    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var telemetryMessages = await _queueService.ReadMessages();

            foreach (var message in telemetryMessages)
            {
                try
                {
                    var messageJson = JsonDocument.Parse(message.Body.ToString());

                    _logger.LogInformation("Message received: {0}", messageJson);

                    var canGetType = messageJson.RootElement.TryGetProperty("message_type", out var messageType);

                    if (canGetType)
                    {
                        await ProcessTelemetryMessage(messageType.GetString(), messageJson);
                    }

                    await _queueService.CompleteMessage(message);

                }
                catch (Exception ex)
                {
                    await _queueService.CompleteMessage(message);
                }
            }
        }
    }

    private Task ProcessTelemetryMessage(string messageType, JsonDocument messageJson)
        => messageType switch
        {
            DeviceTelemetryMessageTypes.StepCompletion => ProccessStepCompletionMessage(messageJson),
            DeviceTelemetryMessageTypes.SessionResult => ProccessSessionResultMessage(messageJson),
            _ => throw new NotImplementedException()
        };

    private async Task ProccessStepCompletionMessage(JsonDocument messageJson)
    {
        var message = JsonSerializer.Deserialize<StepPassTelemetry>(messageJson);
        await _deviceTelemetryService.SaveStepCompletionTelemetry(_mapper.Map<StepPassTelemetryDTO>(message));
    }

    private async Task ProccessSessionResultMessage(JsonDocument messageJson)
    {
        var message = JsonSerializer.Deserialize<SessionResultTelemetry>(messageJson);
        await _deviceTelemetryService.SaveSessionResultTelemetry(_mapper.Map<SessionResultTelemetryDTO>(message));
    }
}
