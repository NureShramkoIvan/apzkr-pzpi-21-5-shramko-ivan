using DroneTrainer.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels;

public sealed class OrganizationDeviceCreateVM
{
    [EnumDataType(typeof(DeviceType))]
    public DeviceType Type { get; set; }
}
