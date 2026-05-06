using Athan.UI.Features.Shell.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Settings;

internal sealed partial class SettingsViewModel : FooterViewModel
{
    [ObservableProperty]
    public partial bool Startup { get; set; }

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
        Startup = _settingsService.Get(Startup, AthanSerializerContext.Default.Boolean);
    }

    partial void OnStartupChanged(bool value)
    {
        _settingsService.Set(value, AthanSerializerContext.Default.Boolean);
    }
}