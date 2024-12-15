using System.Windows;
using Athan.Desktop.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Desktop;

public sealed partial class App
{
    private readonly IServiceProvider services = new ServiceCollection()
        .AddTransient<ShellView>()
        .AddTransient<ShellViewModel>()
        .AddTransient<WelcomeView>()
        .AddTransient<WelcomeViewModel>()
        .BuildServiceProvider();

    protected override void OnStartup(StartupEventArgs eventArgs)
    {
        base.OnStartup(eventArgs);

        MainWindow = services.GetRequiredService<ShellView>();
        MainWindow.Show();
    }
}