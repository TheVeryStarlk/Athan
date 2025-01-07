using System;
using System.Diagnostics;
using System.Windows;
using Athan.Avalonia;
using Avalonia;


try
{
    AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .WithInterFont()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(args);
}
catch (Exception exception)
{
    if (Debugger.IsAttached)
    {
        Debugger.Break();
    }
    else
    {
        MessageBox.Show(
            $"A fatal error has occured." +
            $"{Environment.NewLine}" +
            $"{exception.Message}",
            "Athan",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}