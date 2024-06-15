using DroneTrainer.Core.Enums;

namespace DroneTrainer.Api.ViewModels;

public class OrganizationDeviceVM
{
    public int Id { get; set; }
    public DeviceType Type { get; set; }
    public int OrganizationId { get; set; }
}
