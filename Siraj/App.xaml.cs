using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Serilog;
using Siraj.Features.Settings;
using Siraj.Features.Shell;

namespace Siraj;

public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();

        UnhandledException += OnUnhandledException;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        var settingsService = Bootstrapper.Services.GetRequiredService<SettingsService>();
        var themeService = Bootstrapper.Services.GetRequiredService<ThemeService>();
        var shellView = Bootstrapper.Services.GetRequiredService<ShellView>();

        themeService.Set(settingsService.Theme);
        shellView.Activate();
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
    {
        Log.Fatal(eventArgs.Exception, "Siraj terminated unexpectedly");
    }
}