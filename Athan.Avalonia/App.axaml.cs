using System;
using System.Net.Http;
using Athan.Avalonia.Services;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    public new static App Current => (App?) Application.Current!;

    public IServiceProvider Services { get; }

    private IClassicDesktopStyleApplicationLifetime? lifetime;

    public App()
    {
        Services = new ServiceCollection()
            .AddTransient<ShellView>()
            .AddTransient<ShellViewModel>()
            .AddTransient<PrayerService>()
            .AddSingleton<HttpClient>()
            .BuildServiceProvider();
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
        var window = lifetime?.MainWindow ?? throw new NullReferenceException();

        window.WindowState = window.WindowState is WindowState.Minimized
            ? WindowState.Normal
            : window.WindowState;
    }

    private void CloseTrayIcon(object? sender, EventArgs eventArgs)
    {
        lifetime?.Shutdown();
    }
}