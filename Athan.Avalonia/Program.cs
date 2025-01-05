using System;
using System.Diagnostics;
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
}