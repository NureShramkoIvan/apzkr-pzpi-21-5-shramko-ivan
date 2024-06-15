using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels;

public sealed class OrganizationCreateVM
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
