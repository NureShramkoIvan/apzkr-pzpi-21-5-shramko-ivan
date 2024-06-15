using Azure.Messaging.ServiceBus;
using DroneTrainer.Training.Telemetry.Inftrastructure.Interfaces;
using DroneTrainer.Training.Telemetry.Inftrastructure.Settings;
using Microsoft.Extensions.Options;

namespace DroneTrainer.Training.Telemetry.Inftrastructure.Services;

internal sealed class QueueService : IQueueService
{
    private readonly DeviceTelemetryQueueSettings _deviceTelemetryQueueSettings;
    private readonly ServiceBusReceiver _messageReceiver;

    public QueueService(
        ServiceBusClient serviceBusClient,
        IOptions<DeviceTelemetryQueueSettings> deviceTelemetryQueueSettings)
    {
        _deviceTelemetryQueueSettings = deviceTelemetryQueueSettings.Value;
        _messageReceiver = serviceBusClient.CreateReceiver(_deviceTelemetryQueueSettings.QueueName);
    }

    public async Task<IEnumerable<ServiceBusReceivedMessage>> ReadMessages()
    {
        var messages = await _messageReceiver.ReceiveMessagesAsync(
            _deviceTelemetryQueueSettings.MaxMessagesCount,
            TimeSpan.FromSeconds(_deviceTelemetryQueueSettings.PollingIntervalSeconds));

        return messages;
    }

    public async Task CompleteMessage(ServiceBusReceivedMessage message)
    {
        await _messageReceiver.CompleteMessageAsync(message);
    }
}
