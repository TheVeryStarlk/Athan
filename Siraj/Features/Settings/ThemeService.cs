using System;
using Windows.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Siraj.Features.Shell;

namespace Siraj.Features.Settings;

internal sealed class ThemeService
{
    private Window? window;

    public void Set(Theme theme)
    {
        window ??= Bootstrapper.Services.GetRequiredService<ShellView>();

        var content = (FrameworkElement) window.Content;

        content.RequestedTheme = theme switch
        {
            Theme.System => ElementTheme.Default,
            Theme.Dark => ElementTheme.Dark,
            Theme.Light => ElementTheme.Light,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var foreground = content.ActualTheme is ElementTheme.Dark ? Colors.White : Colors.Black;

        window.AppWindow.TitleBar.ButtonForegroundColor = foreground;
        window.AppWindow.TitleBar.ButtonHoverForegroundColor = foreground;

        var background = content.ActualTheme is ElementTheme.Dark ? Color.FromArgb(24, 255, 255, 255) : Color.FromArgb(24, 0, 0, 0);

        window.AppWindow.TitleBar.ButtonHoverBackgroundColor = background;
    }
}

internal enum Theme
{
    System,
    Dark,
    Light
}