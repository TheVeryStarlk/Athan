using Athan.Avalonia.Messages;
using Athan.Avalonia.Services;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    public new static App Current => (App?) Application.Current!;

    public static string Directory =>
        Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(Athan));

    public IServiceProvider Services { get; }

    private IClassicDesktopStyleApplicationLifetime? lifetime;

    public App()
    {
        Name = nameof(Athan);

        Services = new ServiceCollection()
            // View-models
            .AddSingleton<DashboardViewModel>()
            .AddTransient<DialogViewModel>()
            .AddTransient<LocationViewModel>()
            .AddTransient<OfflineViewModel>()
            .AddTransient<PrayersViewModel>()
            .AddTransient<SettingsViewModel>()
            .AddTransient<ShellViewModel>()

            // Views
            .AddTransient<ShellView>()

            // Other
            .AddTransient<LocationService>()
            .AddSingleton<NavigationService>()
            .AddTransient<NotificationService>()
            .AddTransient<PollService>()
            .AddTransient<PrayerService>()
            .AddSingleton<SettingService>()
            .AddTransient<ThemeService>()
            .AddSingleton<HttpClient>()
            .BuildServiceProvider();

        System.IO.Directory.CreateDirectory(Directory);
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<ShellView>();
            lifetime = desktop;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OpenTrayIcon(object? sender, EventArgs eventArgs)
    {
        WeakReferenceMessenger.Default.Send<TrayIconOpenedMessage>();
    }

    private void CloseTrayIcon(object? sender, EventArgs eventArgs)
    {
        lifetime?.TryShutdown();
    }
}