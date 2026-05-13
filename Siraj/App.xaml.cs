using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Siraj.Features.Settings;
using Siraj.Features.Shell;

namespace Siraj;

public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        var theme = Bootstrapper.Services.GetRequiredService<SettingsService>().Get(Theme.System, SirajSerializerContext.Default.Theme);

        Bootstrapper.Services.GetRequiredService<ThemeService>().Set(theme);

        Bootstrapper.Services.GetRequiredService<ShellView>().Activate();
    }
}