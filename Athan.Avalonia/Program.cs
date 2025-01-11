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
    Log.Fatal(exception, "A fatal exception occured.");

    if (Debugger.IsAttached)
    {
        Debugger.Break();
    }
    else
    {
        MessageBox.Show(
            "Oh no! Something went wrong, please restart the application. Check the logs for more details.",
            "Athan",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}
finally
{
    Log.CloseAndFlush();
}