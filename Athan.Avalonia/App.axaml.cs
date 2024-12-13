using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;

namespace Athan.Avalonia;

internal sealed class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var toRemove = BindingPlugins.DataValidators
            .OfType<DataAnnotationsValidationPlugin>()
            .ToArray();

        foreach (var plugin in toRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            desktop.MainWindow = new ShellView
            {
                DataContext = new ShellViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}