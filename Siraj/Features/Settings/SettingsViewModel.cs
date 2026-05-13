using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Siraj.Features.Prayers.Calculation;
using Siraj.Features.Shell.Items;

namespace Siraj.Features.Settings;

internal sealed partial class SettingsViewModel : FooterViewModel
{
    [ObservableProperty]
    public partial int ThemeIndex { get; set; }

    [ObservableProperty]
    public partial int ReciterIndex { get; set; }

    [ObservableProperty]
    public partial int CalculationIndex { get; set; }

    [ObservableProperty]
    public partial bool Startup { get; set; }

    private readonly SettingsService settingsService;
    private readonly ThemeService themeService;
    private readonly StartupService startupService;

    public SettingsViewModel(SettingsService settingsService, ThemeService themeService, StartupService startupService)
    {
        this.settingsService = settingsService;
        this.themeService = themeService;
        this.startupService = startupService;

        Glyph = "\uE713";
        Title = "Settings";
    }

    [RelayCommand]
    private void Initialize()
    {
        // Write a converter?
        ThemeIndex = (int) settingsService.Theme;
        ReciterIndex = (int) settingsService.Reciter;
        CalculationIndex = (int) settingsService.Calculation;

        Startup = settingsService.Startup;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (await startupService.TryToggle(Startup))
        {
            settingsService.Startup = Startup;
        }
    }

    partial void OnThemeIndexChanged(int value)
    {
        var theme = settingsService.Theme = (Theme) value;

        themeService.Set(theme);
    }

    partial void OnReciterIndexChanged(int value)
    {
        settingsService.Reciter = (CallReciter) value;
    }

    partial void OnCalculationIndexChanged(int value)
    {
        settingsService.Calculation = (PrayerCalculation) value;
    }
}

internal enum CallReciter
{
    MisharyAlAfasy,
    Madinah,
    Makkah
}