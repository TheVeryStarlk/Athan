using Athan.UI.Features.Shell;

namespace Athan.UI.Features.Settings;

internal sealed partial class SettingsViewModel : FooterViewModel
{
    public SettingsViewModel()
    {
        Glyph = "\uE713";
        Title = "Settings";
    }
}