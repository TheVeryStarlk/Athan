using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Prayers;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Welcome;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellView
{
    private readonly ShellViewModel viewModel;

    public ShellView(
        ShellViewModel viewModel,
        NavigationService navigationService,
        SnackbarService snackbarService,
        WelcomeView welcomeView,
        PrayersView prayersView,
        SettingsView settingsView,
        OfflineView offlineView)
    {
        this.viewModel = viewModel;
        DataContext = viewModel;

        navigationService.Navigated += destination =>
        {
            Shell.Content = destination switch
            {
                Destination.Welcome => welcomeView,
                Destination.Prayers => prayersView,
                Destination.Settings => settingsView,
                Destination.Offline => offlineView,
                _ => throw new ArgumentOutOfRangeException()
            };

            MainGrid.Visibility = Visibility.Visible;
        };

        InitializeComponent();

        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }

    protected override async void OnInitialized(EventArgs eventArgs)
    {
        try
        {
            base.OnInitialized(eventArgs);
            await viewModel.InitializeAsync();
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }

    protected override void OnClosing(CancelEventArgs eventArgs)
    {
        base.OnClosing(eventArgs);
        SystemThemeWatcher.UnWatch(this);
    }
}