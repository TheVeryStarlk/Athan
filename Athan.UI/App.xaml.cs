using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Athan.UI;

public sealed partial class App : Application
{
    public static ServiceProvider Services { get; } = Bootstrapper.Build();

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Services
            .GetRequiredService<ShellView>()
            .Activate();
    }
}