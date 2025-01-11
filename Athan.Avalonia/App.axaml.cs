using System;
using System.Linq;
using Athan.Avalonia.Features.Shell;
using Athan.Services;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    private readonly IServiceProvider services = Bootstrapper.Build();

    public override void Initialize()
    {
        DataTemplates.Add(services.GetRequiredService<ViewLocator>());
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var plugins = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in plugins)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }

        var storage = services.GetRequiredService<StorageService>();

        storage.Initialize();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += (_, _) =>
            {
                storage.Save();
                WeakReferenceMessenger.Default.Send<Closing>();
            };

            desktop.MainWindow = services.GetRequiredService<ShellView>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void TrayIconOnClicked(object? sender, EventArgs eventArgs)
    {
        var window = ((IClassicDesktopStyleApplicationLifetime) ApplicationLifetime!).MainWindow!;

        window.ShowInTaskbar = true;
        window.WindowState = WindowState.Normal;
    }
}