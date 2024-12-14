using System.Reflection;
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
        const string name = ".lock";

        if (File.Exists(name))
        {
            File.WriteAllBytes(name, [byte.MaxValue]);
            Exit();

            return;
        }

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

        File.WriteAllBytes(name, []);

        var watcher = new FileSystemWatcher(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "*.lock")
        {
            NotifyFilter = NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };

        watcher.Changed += (_, _) => window.Show();

        window.Closed += (_, _) =>
        {
            File.Delete(name);
            watcher.Dispose();
        };
    }
}