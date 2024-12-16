using System.ComponentModel;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Welcome;
using Wpf.Ui.Appearance;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellView
{
    private readonly ShellViewModel viewModel;

    public ShellView(
        ShellViewModel viewModel,
        NavigationService navigationService,
        WelcomeView welcomeView,
        SettingsView settingsView,
        OfflineView offlineView)
    {
        this.viewModel = viewModel;

        DataContext = viewModel;

        navigationService.Navigated += destination => Shell.Content = destination switch
        {
            Destination.Welcome => welcomeView,
            Destination.Settings => settingsView,
            Destination.Offline => offlineView,
            _ => throw new ArgumentOutOfRangeException()
        };

        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs eventArgs)
    {
        base.OnInitialized(eventArgs);
        viewModel.Initialize();
    }

    protected override void OnClosing(CancelEventArgs eventArgs)
    {
        base.OnClosing(eventArgs);
        SystemThemeWatcher.UnWatch(this);
    }
}