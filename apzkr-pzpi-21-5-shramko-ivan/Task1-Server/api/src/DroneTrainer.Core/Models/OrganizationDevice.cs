using DroneTrainer.Core.Enums;

namespace DroneTrainer.Core.Models;

public sealed class OrganizationDevice
{
    public int Id { get; set; }
    public DeviceType Type { get; set; }
    public int OrganizationId { get; set; }
}
