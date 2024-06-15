using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels.Authentication;

public sealed class TokenGetVM
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
