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
        var settingsService = Bootstrapper.Services.GetRequiredService<SettingsService>();

        Bootstrapper.Services.GetRequiredService<ThemeService>().Set(settingsService.Theme);

        Bootstrapper.Services.GetRequiredService<ShellView>().Activate();
    }
}