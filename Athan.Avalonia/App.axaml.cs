using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    public new static App Current => (App?) Application.Current!;

    public IServiceProvider Services { get; }

    public App()
    {
        Services = new ServiceCollection()
            .AddTransient<ShellView>()
            .AddTransient<ShellViewModel>()
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
        }

        base.OnFrameworkInitializationCompleted();
    }
}