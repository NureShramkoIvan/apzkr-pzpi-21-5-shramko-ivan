using DroneTrainer.Core.Enums;
using DroneTrainer.Shared.Dates.Types.Enums;

namespace DroneTrainer.Infrastructure.DTOs.Authenitication;

public sealed class UserRegisterDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public int OrganizationId { get; set; }
    public SupportedTimeZone UserTimeZone { get; set; }
}
