using Athan.UI.Features.Shell.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Athan.UI.Features.Settings;

internal sealed partial class SettingsViewModel : FooterViewModel
{
    [ObservableProperty]
    public partial int ThemeIndex { get; set; }

    [ObservableProperty]
    public partial int ReciterIndex { get; set; }

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
        ThemeIndex = (int) settingsService.Get(Theme.System, AthanSerializerContext.Default.Theme);
        ReciterIndex = (int) settingsService.Get(Reciter.MisharyAlAfasy, AthanSerializerContext.Default.Reciter);
        LaunchStartup = settingsService.Get(LaunchStartup, AthanSerializerContext.Default.Boolean, nameof(LaunchStartup));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        var result = await startupService.TryToggle(LaunchStartup);
        settingsService.Set(result, AthanSerializerContext.Default.Boolean, nameof(LaunchStartup));
    }

    partial void OnThemeIndexChanged(int value)
    {
        var theme = (Theme) value;

        settingsService.Set(theme, AthanSerializerContext.Default.Theme);
        themeService.Set(theme);
    }

    partial void OnReciterIndexChanged(int value)
    {
        settingsService.Set((Reciter) value, AthanSerializerContext.Default.Reciter);
    }
}

internal enum Reciter
{
    MisharyAlAfasy,
    Madinah,
    Makkah
}