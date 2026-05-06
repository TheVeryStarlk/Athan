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
        Bootstrapper.Services.GetRequiredService<ShellView>().Activate();
    }
}