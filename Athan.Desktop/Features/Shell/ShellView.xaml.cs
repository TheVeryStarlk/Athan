using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
        NotificationService notificationService,
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
            UserControl view = destination switch
            {
                Destination.Welcome => welcomeView,
                Destination.Prayers => prayersView,
                Destination.Settings => settingsView,
                Destination.Offline => offlineView,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (destination is Destination.Settings)
            {
                SettingsShell.Content = view;
                SettingsShell.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                Shell.Content = view;
                SettingsShell.Visibility = Visibility.Collapsed;
                MainGrid.Visibility = Visibility.Visible;
            }
        };

        notificationService.NotificationActivated += () => Dispatcher.Invoke(() => WindowState = WindowState.Normal);

        InitializeComponent();

        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }

    protected override async void OnPropertyChanged(DependencyPropertyChangedEventArgs eventArgs)
    {
        try
        {
            base.OnPropertyChanged(eventArgs);

            if (eventArgs.Property != WindowStateProperty)
            {
                return;
            }

            if (WindowState is WindowState.Minimized)
            {
                ShowInTaskbar = false;
                await viewModel.OnMinimizedAsync();
            }
            else
            {
                ShowInTaskbar = true;
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
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

    protected override async void OnClosing(CancelEventArgs eventArgs)
    {
        try
        {
            base.OnClosing(eventArgs);
            await viewModel.OnClosingAsync();

            SystemThemeWatcher.UnWatch(this);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}