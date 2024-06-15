using DroneTrainer.Mobile.Application.Resources.Strings;
using System.ComponentModel;
using System.Globalization;

namespace DroneTrainer.Mobile.Application;

public sealed class LocalizationResourseManager : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public event EventHandler LocaleChaged;

    public object this[string resourseKey]
        => AppResources.ResourceManager.GetObject(resourseKey, AppResources.Culture) ?? Array.Empty<byte>();

    public static LocalizationResourseManager Instance { get; } = new();

    private LocalizationResourseManager()
    {
        AppResources.Culture = new CultureInfo("uk-UA");
    }

    public void SetCulture(CultureInfo culture)
    {
        AppResources.Culture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        LocaleChaged?.Invoke(this, new EventArgs());
    }
}
