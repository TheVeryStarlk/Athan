using System;
using System.Linq;
using Athan.Avalonia.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    private readonly IServiceProvider services = Bootstrapper.Build();

    public App()
    {
        DataTemplates.Add(services.GetRequiredService<ViewLocator>());
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var plugins = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in plugins)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = services.GetRequiredService<ShellView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}