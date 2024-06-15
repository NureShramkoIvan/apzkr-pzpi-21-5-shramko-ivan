namespace DroneTrainer.Training.Telemetry.Inftrastructure.Settings;

internal sealed class DeviceTelemetryQueueSettings
{
    public string QueueName { get; set; }
    public int MaxMessagesCount { get; set; }
    public int PollingIntervalSeconds { get; set; }
}
