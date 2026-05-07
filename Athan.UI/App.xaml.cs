using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Athan.UI;

public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        var theme = Bootstrapper.Services.GetRequiredService<SettingsService>().Get(Theme.System, AthanSerializerContext.Default.Theme);

        Bootstrapper.Services.GetRequiredService<ThemeService>().Set(theme);

        Bootstrapper.Services.GetRequiredService<ShellView>().Activate();
    }
}