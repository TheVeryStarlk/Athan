using System;
using System.Net.Http;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Services;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;
using Athan.Services;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    public new static App Current => (App?) Application.Current!;

    public IServiceProvider Services { get; }

    private IClassicDesktopStyleApplicationLifetime? lifetime;

    public App()
    {
        Name = nameof(Athan);

        Services = new ServiceCollection()
            // View-models
            .AddSingleton<DashboardViewModel>()
            .AddTransient<LocationViewModel>()
            .AddTransient<OfflineViewModel>()
            .AddTransient<PrayersViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddTransient<ShellViewModel>()

            // Views
            .AddTransient<ShellView>()

            // Other
            .AddTransient<LocationService>()
            .AddSingleton<NavigationService>()
            .AddTransient<NotificationService>()
            .AddTransient<PollyService>()
            .AddTransient<PrayerService>()
            .AddTransient<SettingsService>()
            .AddTransient<ThemeService>()
            .AddSingleton<HttpClient>()
            .BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<ShellView>();
            lifetime = desktop;

            // To show the correct locally saved theme almost before anything runs
            var setting = await Services.GetRequiredService<SettingsService>().ReadAsync();
            Services.GetRequiredService<ThemeService>().Update(setting?.Theme ?? FluentThemeMode.Light);
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