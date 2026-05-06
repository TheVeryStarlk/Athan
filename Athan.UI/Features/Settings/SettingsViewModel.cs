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

    private readonly SettingsService _settingsService;
    
    public SettingsViewModel(SettingsService settingsService)
    {
        _settingsService = settingsService;

        Glyph = "\uE713";
        Title = "Settings";
    }

    [RelayCommand]
    private void Initialize()
    {
        ThemeIndex = (int) _settingsService.Get(Theme.System, AthanSerializerContext.Default.Theme);
        ReciterIndex = (int) _settingsService.Get(Reciter.MisharyAlAfasy, AthanSerializerContext.Default.Reciter);
        LaunchStartup = _settingsService.Get(LaunchStartup, AthanSerializerContext.Default.Boolean, nameof(LaunchStartup));
    }

    partial void OnThemeIndexChanged(int value)
    {
        _settingsService.Set((Theme) value, AthanSerializerContext.Default.Theme);
    }

    partial void OnReciterIndexChanged(int value)
    {
        _settingsService.Set((Reciter) value, AthanSerializerContext.Default.Reciter);
    }
    
    partial void OnLaunchStartupChanged(bool value)
    {
        _settingsService.Set(value, AthanSerializerContext.Default.Boolean, nameof(LaunchStartup));
    }
}

internal enum Theme
{
    System,
    Dark,
    Light
}

internal enum Reciter
{
    MisharyAlAfasy,
    Madinah,
    Makkah
}