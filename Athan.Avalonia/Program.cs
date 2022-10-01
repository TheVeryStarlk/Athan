using System;
using Avalonia;

namespace Athan.Avalonia;

internal sealed class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .StartWithClassicDesktopLifetime(args);
    }
}