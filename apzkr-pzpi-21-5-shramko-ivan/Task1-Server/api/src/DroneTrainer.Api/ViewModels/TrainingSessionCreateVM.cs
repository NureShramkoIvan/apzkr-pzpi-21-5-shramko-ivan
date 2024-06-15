using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels;

public sealed class TrainingSessionCreateVM : IValidatableObject
{
    [Required]
    public DateTimeOffset ScheduledAt { get; set; }

    [Required]
    public int ProgramId { get; set; }

    [Required]
    public int InstructorId { get; set; }

    [Required]
    public int GroupId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ScheduledAt <= DateTime.UtcNow)
        {
            yield return new("Invalid Session date and time");
        }
    }
}
