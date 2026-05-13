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
    public partial bool LaunchStartup { get; set; }

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
        ThemeIndex = (int) settingsService.Get(Theme.System, SirajSerializerContext.Default.Theme);
        ReciterIndex = (int) settingsService.Get(CallReciter.MisharyAlAfasy, SirajSerializerContext.Default.CallReciter);
        CalculationIndex = (int) settingsService.Get(PrayerCalculation.Makkah, SirajSerializerContext.Default.PrayerCalculation);
        LaunchStartup = settingsService.Get(LaunchStartup, SirajSerializerContext.Default.Boolean, nameof(LaunchStartup));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (await startupService.TryToggle(LaunchStartup))
        {
            settingsService.Set(LaunchStartup, SirajSerializerContext.Default.Boolean, nameof(LaunchStartup));
        }
    }

    partial void OnThemeIndexChanged(int value)
    {
        var theme = (Theme) value;

        settingsService.Set(theme, SirajSerializerContext.Default.Theme);
        themeService.Set(theme);
    }

    partial void OnReciterIndexChanged(int value)
    {
        settingsService.Set((CallReciter) value, SirajSerializerContext.Default.CallReciter);
    }

    partial void OnCalculationIndexChanged(int value)
    {
        settingsService.Set((PrayerCalculation) value, SirajSerializerContext.Default.PrayerCalculation);
    }
}

internal enum CallReciter
{
    MisharyAlAfasy,
    Madinah,
    Makkah
}