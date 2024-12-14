using Athan.UI.Features.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Athan.UI;

public sealed partial class App : Application
{
    public static IServiceProvider Services { get; } = Bootstrapper.Create();

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        var shell = Services.GetRequiredService<ShellView>();
        shell.Activate();
    }
}