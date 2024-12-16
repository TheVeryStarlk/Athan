using System.Windows;
using Athan.Desktop.Features.Shell;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Appearance;

namespace Athan.Desktop;

public sealed partial class App
{
    protected override void OnStartup(StartupEventArgs eventArgs)
    {
        base.OnStartup(eventArgs);

        var services = Bootstrapper.Create();
        var view = services.GetRequiredService<ShellView>();

        SystemThemeWatcher.Watch(view);

        MainWindow = view;
        MainWindow.Show();
    }
}