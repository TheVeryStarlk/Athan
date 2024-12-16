using System.Windows;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;
using SettingsViewModel = Athan.Desktop.Features.Settings.SettingsViewModel;

namespace Athan.Desktop;

public sealed partial class App
{
    private readonly IServiceProvider services = new ServiceCollection()
        .AddTransient<ShellView>()
        .AddTransient<ShellViewModel>()
        .AddFactory<WelcomeView, WelcomeViewModel>()
        .AddTransient<WelcomeViewModel>()
        .AddFactory<SettingsView, SettingsViewModel>()
        .AddTransient<SettingsViewModel>()
        .AddSingleton<UserControlFactory>()
        .AddFactory<WelcomeView, WelcomeViewModel>()
        .BuildServiceProvider();

    protected override void OnStartup(StartupEventArgs eventArgs)
    {
        base.OnStartup(eventArgs);

        MainWindow = services.GetRequiredService<ShellView>();
        MainWindow.Show();
    }
}