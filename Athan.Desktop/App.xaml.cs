using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Desktop;

public sealed partial class App
{
    private readonly IServiceProvider services = new ServiceCollection()
        .AddTransient<ShellView>()
        .BuildServiceProvider();

    protected override void OnStartup(StartupEventArgs eventArgs)
    {
        base.OnStartup(eventArgs);

        MainWindow = services.GetRequiredService<ShellView>();
        MainWindow.Show();
    }
}