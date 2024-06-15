using DroneTrainer.Core.Enums;

namespace DroneTrainer.Infrastructure.DTOs;

public sealed class OrganizationDeviceDTO
{
    public int Id { get; set; }
    public DeviceType Type { get; set; }
    public int OrganizationId { get; set; }
}
