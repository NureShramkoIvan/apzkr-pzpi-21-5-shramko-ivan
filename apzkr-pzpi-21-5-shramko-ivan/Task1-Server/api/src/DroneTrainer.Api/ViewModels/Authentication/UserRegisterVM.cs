using DroneTrainer.Api.Validation.Attributes;
using DroneTrainer.Core.Enums;
using DroneTrainer.Shared.Dates.Types.Enums;
using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels.Authentication;

public sealed class UserRegisterVM
{
    [Required]
    [MinLength(6)]
    [MaxLength(12)]
    [RegularExpression("^[A-Za-z0-9_-]*$")]
    public string UserName { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    [PasswordValidator]
    public string Password { get; set; }

    [Required]
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }

    [Required]
    public int OrganizationId { get; set; }

    [EnumDataType(typeof(SupportedTimeZone))]
    public SupportedTimeZone UserTimeZone { get; set; }
}
