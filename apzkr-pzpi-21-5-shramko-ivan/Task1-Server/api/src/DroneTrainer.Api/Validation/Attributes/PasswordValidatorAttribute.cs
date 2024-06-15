using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DroneTrainer.Api.Validation.Attributes;

internal sealed partial class PasswordValidatorAttribute : ValidationAttribute
{
    [GeneratedRegex("[a-z]+")]
    private partial Regex _lower();

    [GeneratedRegex("[A-Z]+")]
    private partial Regex _upper();

    [GeneratedRegex("[0-9]+")]
    private partial Regex _numeric();

    [GeneratedRegex("[!@#$%^&*]+")]
    private partial Regex _special();

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not string password) return new ValidationResult("Invalid password value.");

        if (!_lower().IsMatch(password)) return new ValidationResult("Password must containe lowercase letters.");

        if (!_upper().IsMatch(password)) return new ValidationResult("Password must contain uppercase letters.");

        if (!_numeric().IsMatch(password)) return new ValidationResult("Password must contain digits.");

        if (!_special().IsMatch(password)) return new ValidationResult("Password must conatin one of this special characters(! @ # $ % ^ & *).");

        return ValidationResult.Success;
    }
}
