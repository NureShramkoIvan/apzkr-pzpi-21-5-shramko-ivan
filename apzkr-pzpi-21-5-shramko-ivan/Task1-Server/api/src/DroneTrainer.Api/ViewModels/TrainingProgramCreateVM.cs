using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.ViewModels;

public sealed class TrainingProgramCreateVM : IValidatableObject
{
    public IEnumerable<TrainingProgramStepVM> Steps { get; set; }

    [Required]
    public int OrganizationId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Steps.Any()) yield return new("Program must containt a least one step");

        var distinctCount = Steps.DistinctBy(s => s.DeviceId).Count();

        if (distinctCount < Steps.Count()) yield return new("Program must not conatin dublicate Device ID entries");

        var position = 1;
        foreach (var step in Steps.OrderByDescending(s => s.Position))
        {
            if (step.Position != position)
            {
                yield return new("Devivce positions are incorrect");
                break;
            }

            position += 1;
        }
    }
}
