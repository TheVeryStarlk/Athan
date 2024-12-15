using Athan.UI.Features.Shell;
using Athan.UI.Features.Welcome;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Athan.UI;

public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        var services = Bootstrapper.Create();

        var window = new Window
        {
            Content = services.GetRequiredService<ShellView>(),
            ExtendsContentIntoTitleBar = true,
            SystemBackdrop = new MicaBackdrop()
        };

        var request = new NavigationRequest(services.GetRequiredService<WelcomeViewModel>());
        WeakReferenceMessenger.Default.Send(request);

        window.Activate();
    }
}