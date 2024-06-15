using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels;

public sealed class TrainingGroupCreateVM
{
    [Required]
    [MaxLength(10)]
    [RegularExpression("^[0-9a-zA-Z_-]+$")]
    public string Name { get; set; }

    [Required]
    public int OrganizationId { get; set; }
}
