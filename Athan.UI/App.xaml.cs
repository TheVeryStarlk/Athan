using Microsoft.UI.Xaml;

namespace Athan.UI;

public sealed partial class App : Application
{
    private Window? window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        window = new MainWindow();
        window.Activate();
    }
}