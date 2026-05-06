using Athan.UI.Features.Shell.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    public SettingsViewModel(SettingsService settingsService, ThemeService themeService)
    {
        this.settingsService = settingsService;
        this.themeService = themeService;

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
    
    partial void OnLaunchStartupChanged(bool value)
    {
        settingsService.Set(value, AthanSerializerContext.Default.Boolean, nameof(LaunchStartup));
    }
}

internal enum Reciter
{
    MisharyAlAfasy,
    Madinah,
    Makkah
}