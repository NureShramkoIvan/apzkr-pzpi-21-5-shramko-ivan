using Microsoft.Extensions.Localization;

namespace DroneTrainer.Api.Localization.Services;

public sealed class ErrorMessageLocalizer(IStringLocalizer<ErrorMessageLocalizer> localizer)
{
    private readonly IStringLocalizer<ErrorMessageLocalizer> _localizer = localizer;

    public string GetLocalizedErrorMessage(string error) => _localizer[error];
}
