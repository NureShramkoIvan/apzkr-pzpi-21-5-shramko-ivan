using DroneTrainer.Core.Enums;
using DroneTrainer.Shared.Dates.Types.Enums;

namespace DroneTrainer.Core.Models;

public sealed class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Password { get; set; }
    public int OrganizationId { get; set; }
    public Role Role { get; set; }
    public SupportedTimeZone UserTimeZone { get; set; }
}
