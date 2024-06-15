using Azure.Messaging.ServiceBus;

namespace DroneTrainer.Training.Telemetry.Inftrastructure.Interfaces;

public interface IQueueService
{
    Task<IEnumerable<ServiceBusReceivedMessage>> ReadMessages();
    Task CompleteMessage(ServiceBusReceivedMessage message);
}
