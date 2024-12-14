using Athan.UI.Features.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

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
        var window = new Window
        {
            Content = Services.GetRequiredService<ShellView>(),
            ExtendsContentIntoTitleBar = true,
            SystemBackdrop = new MicaBackdrop()
        };

        window.Activate();
    }
}