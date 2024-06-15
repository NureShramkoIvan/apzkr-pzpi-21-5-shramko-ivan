using DroneTrainer.Mobile.Core.Enums;

namespace DroneTrainer.Mobile.Core.Models;

public sealed class OrganizationDevice
{
    public int Id { get; set; }
    public DeviceType Type { get; set; }
    public int OrganizationId { get; set; }
    public string DeviceUniqueId { get; set; }
}
