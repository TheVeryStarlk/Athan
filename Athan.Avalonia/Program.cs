using System;
using System.Diagnostics;
using System.Windows;
using Athan.Avalonia;
using Avalonia;
using Serilog;

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
    Log.Fatal(exception, "A fatal error occured.");

    if (Debugger.IsAttached)
    {
        Debugger.Break();
    }
    else
    {
        MessageBox.Show(
            $"A fatal error has occured. Please restart Athan." +
            $"{Environment.NewLine}" +
            $"{exception.Message}",
            "Athan",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}
finally
{
    Log.CloseAndFlush();
}